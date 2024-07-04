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
            IStateRepository cityRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._stateRepository = cityRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserId>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.UserExists(new Email(request.Email));
            if (!userExists)
            {
                return Result.Failure<UserId>(UserErrors.DuplicateEmail);
            }

            State? state = await this._stateRepository.GetByIdAsync(
                request.StateId,
                cancellationToken
            );
            if (state is null)
            {
                return Result.Failure<UserId>(StateErrors.NotFound);
            }

            Result<User> result = User.Create(
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.BirthDate,
                new Email(request.Email),
                new PhoneNumber(request.PhoneNumber)
            );

            if (result.IsFailure)
            {
                return Result.Failure<UserId>(result.Error);
            }

            User user = result.Value;

            string identityId = await this._authenticationService.RegisterAsync(
                user,
                request.Password,
                cancellationToken
            );

            user.SetState(state);

            user.SetIdentityId(identityId);

            this._userRepository.Add(user);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
