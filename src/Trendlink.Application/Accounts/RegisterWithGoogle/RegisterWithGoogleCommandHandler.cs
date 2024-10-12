using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Accounts.LogIn;
using Trendlink.Application.Exceptions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.RegisterWithGoogle
{
    internal sealed class RegisterWithGoogleCommandHandler
        : ICommandHandler<RegisterWithGoogleCommand, AccessTokenResponse>
    {
        private const string ProviderName = "google";

        private readonly IGoogleService _googleService;
        private readonly IKeycloakService _keycloakService;
        private readonly IAuthenticationService _authenticationService;

        private readonly IUserRepository _userRepository;
        private readonly IStateRepository _stateRepository;

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterWithGoogleCommandHandler(
            IGoogleService googleService,
            IKeycloakService keycloakService,
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IStateRepository stateRepository,
            IDateTimeProvider dateTimeProvider,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._googleService = googleService;
            this._keycloakService = keycloakService;
            this._authenticationService = authenticationService;
            this._userRepository = userRepository;
            this._stateRepository = stateRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            RegisterWithGoogleCommand request,
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

            bool userExistsInKeycloak = await this._keycloakService.CheckUserExistsInKeycloak(
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

                Result linkGoogleResult =
                    await this._keycloakService.LinkExternalIdentityProviderAccountToKeycloakUserAsync(
                        user.IdentityId,
                        ProviderName,
                        userInfo.Id,
                        userInfo.Name,
                        cancellationToken
                    );
                if (linkGoogleResult.IsFailure)
                {
                    return Result.Failure<AccessTokenResponse>(linkGoogleResult.Error);
                }

                Result<AccessTokenResponse> tokenResult =
                    await this._keycloakService.AuthenticateWithGoogleAsync(
                        userInfo,
                        cancellationToken
                    );
                if (tokenResult.IsFailure)
                {
                    return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
                }

                DateTime utcNow = this._dateTimeProvider.UtcNow;
                var emailVerificationToken = new EmailVerificationToken(
                    user.Id,
                    utcNow,
                    utcNow.AddDays(1)
                );
                this._emailVerificationTokenRepository.Add(emailVerificationToken);
                user.VerifyEmail(emailVerificationToken);

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

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
