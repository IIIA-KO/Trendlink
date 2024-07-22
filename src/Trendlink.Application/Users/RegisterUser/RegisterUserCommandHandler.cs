using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserId>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IStateRepository stateRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._stateRepository = stateRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserId>> Handle(
            RegisterUserCommand request,
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

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

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
