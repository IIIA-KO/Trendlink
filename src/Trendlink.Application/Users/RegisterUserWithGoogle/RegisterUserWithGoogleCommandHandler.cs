using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Exceptions;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUserWithGoogle
{
    internal sealed class RegisterUserWithGoogleCommandHandler
        : ICommandHandler<RegisterUserWithGoogleCommand, AccessTokenResponse>
    {
        private const string ProviderName = "google";

        private readonly IGoogleService _googleService;
        private readonly IJwtService _jwtService;
        private readonly IAuthenticationService _authenticationService;

        private readonly IUserRepository _userRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserWithGoogleCommandHandler(
            IGoogleService googleService,
            IJwtService jwtService,
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IStateRepository stateRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._googleService = googleService;
            this._jwtService = jwtService;
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._stateRepository = stateRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            RegisterUserWithGoogleCommand request,
            CancellationToken cancellationToken
        )
        {
            GoogleTokenResponse? accessToken = await this._googleService.GetAccessTokenAsync(
                request.Code,
                cancellationToken
            );
            if (accessToken is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            GoogleUserInfo? userInfo = await this._googleService.GetUserInfoAsync(
                accessToken.AccessToken,
                cancellationToken
            );
            if (userInfo is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            bool stateExists = await this._stateRepository.ExistsByIdAsync(
                request.StateId,
                cancellationToken
            );
            if (!stateExists)
            {
                return Result.Failure<AccessTokenResponse>(StateErrors.NotFound);
            }

            var email = new Email(userInfo.Email);

            bool userExistsInDb = await this._userRepository.ExistByEmailAsync(
                email,
                cancellationToken
            );
            if (userExistsInDb)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.DuplicateEmail);
            }

            bool userExistsInKeycloak = await this._jwtService.CheckUserExistsInKeycloak(
                userInfo.Email,
                cancellationToken
            );
            if (userExistsInKeycloak)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.DuplicateEmail);
            }

            Result<User> result = User.Create(
                new FirstName(userInfo.Name),
                new LastName(userInfo.GivenName),
                request.BirthDate,
                request.StateId,
                email,
                request.PhoneNumber
            );
            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(result.Error);
            }

            User user = result.Value;

            try
            {
                string identityId = await this._authenticationService.RegisterAsync(
                    user,
                    user.Email.Value,
                    cancellationToken
                );

                user.SetIdentityId(identityId);

                this._userRepository.Add(user);

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

                Result linkGoogleResult =
                    await this._jwtService.LinkExternalIdentityProviderAccountToKeycloakUserAsync(
                        user.IdentityId,
                        ProviderName,
                        userInfo.Id,
                        userInfo.Name,
                        cancellationToken
                    );
                if (linkGoogleResult.IsFailure)
                {
                    return Result.Failure<AccessTokenResponse>(result.Error);
                }

                Result<AccessTokenResponse> tokenResult =
                    await this._jwtService.AuthenticateWithGoogleAsync(userInfo, cancellationToken);
                if (tokenResult.IsFailure)
                {
                    return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
                }

                return tokenResult.Value;
            }
            catch (Exception exception)
                when (exception is HttpRequestException
                    || exception is ArgumentNullException
                    || exception is ConcurrencyException
                )
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.RegistrationFailed);
            }
        }
    }
}
