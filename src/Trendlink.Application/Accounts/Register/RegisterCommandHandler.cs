using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.Register
{
    internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserId>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IEmailService emailService,
            IDateTimeProvider dateTimeProvider,
            IEmailVerificationLinkFactory emailVerificationLinkFactory,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IStateRepository stateRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._emailService = emailService;
            this._dateTimeProvider = dateTimeProvider;
            this._emailVerificationLinkFactory = emailVerificationLinkFactory;
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._stateRepository = stateRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserId>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.ExistByEmailAsync(
                request.Email,
                cancellationToken
            );
            if (userExists)
            {
                return Result.Failure<UserId>(UserErrors.DuplicateEmail);
            }

            bool stateExists = await this._stateRepository.ExistsByIdAsync(
                request.StateId,
                cancellationToken
            );
            if (!stateExists)
            {
                return Result.Failure<UserId>(StateErrors.NotFound);
            }

            Result<User> result = User.Create(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                request.StateId,
                request.Email,
                request.PhoneNumber
            );
            if (result.IsFailure)
            {
                return Result.Failure<UserId>(result.Error);
            }

            User user = result.Value;

            try
            {
                string identityId = await this._authenticationService.RegisterAsync(
                    user,
                    request.Password,
                    cancellationToken
                );

                user.SetIdentityId(identityId);

                this._userRepository.Add(user);

                DateTime utcNow = this._dateTimeProvider.UtcNow;
                var emailVerificationToken = new EmailVerificationToken(
                    user.Id,
                    utcNow,
                    utcNow.AddDays(1)
                );
                this._emailVerificationTokenRepository.Add(emailVerificationToken);

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

                string verificationLink = this._emailVerificationLinkFactory.Create(
                    emailVerificationToken
                );

                await this._emailService.SendAsync(
                    user.Email,
                    "Email Verification for Trendlink",
                    $"To verify your email <a href='{verificationLink}'>click here</a>",
                    isHtml: true
                );

                return user.Id;
            }
            catch (Exception exception)
                when (exception is HttpRequestException || exception is ArgumentNullException)
            {
                return Result.Failure<UserId>(UserErrors.RegistrationFailed);
            }
        }
    }
}
