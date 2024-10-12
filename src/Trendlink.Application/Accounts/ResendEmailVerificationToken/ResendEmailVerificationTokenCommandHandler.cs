using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.ResendEmailVerificationToken
{
    internal sealed class ResendEmailVerificationTokenCommandHandler
        : ICommandHandler<ResendEmailVerificationTokenCommand>
    {
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public ResendEmailVerificationTokenCommandHandler(
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IUserRepository userRepository,
            IDateTimeProvider dateTimeProvider,
            IEmailVerificationLinkFactory emailVerificationLinkFactory,
            IEmailService emailService,
            IUnitOfWork unitOfWork
        )
        {
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._userRepository = userRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._emailVerificationLinkFactory = emailVerificationLinkFactory;
            this._emailService = emailService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ResendEmailVerificationTokenCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken
            );
            if (user == null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            if (user.EmailVerified)
            {
                return Result.Failure(EmailVerificationTokenErrors.EmailAlreadyVerified);
            }

            EmailVerificationToken? activeToken =
                await this._emailVerificationTokenRepository.GetActiveTokenByUserId(
                    user.Id,
                    cancellationToken
                );
            if (activeToken != null && activeToken.ExpiresAtUtc > this._dateTimeProvider.UtcNow)
            {
                this._emailVerificationTokenRepository.Remove(activeToken);
            }

            DateTime utcNow = this._dateTimeProvider.UtcNow;
            var newToken = new EmailVerificationToken(user.Id, utcNow, utcNow.AddDays(1));
            this._emailVerificationTokenRepository.Add(newToken);

            string verificationLink = this._emailVerificationLinkFactory.Create(newToken);

            await this._emailService.SendAsync(
                user.Email,
                "Renew Email Verification for Trendlink",
                $"To verify your email <a href='{verificationLink}'>click here</a>",
                isHtml: true
            );

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
