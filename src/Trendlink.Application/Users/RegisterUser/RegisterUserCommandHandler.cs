using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserId>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._cityRepository = cityRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserId>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken
        )
        {
            City? city = await this._cityRepository.GetByIdAsync(request.CityId, cancellationToken);
            if (city is null)
            {
                return Result.Failure<UserId>(CityErrors.NotFound);
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

            user.SetCity(city);

            user.SetIdentityId(identityId);

            this._userRepository.Add(user);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
