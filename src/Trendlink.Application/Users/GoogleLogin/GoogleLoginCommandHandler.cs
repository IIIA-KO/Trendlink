using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.GoogleLogin
{
    internal sealed class GoogleLoginCommandHandler
        : ICommandHandler<GoogleLogInUserCommand, AccessTokenResponse>
    {
        private readonly IGoogleService _googleService;
        private readonly IJwtService _jwtService;

        public GoogleLoginCommandHandler(IGoogleService googleService, IJwtService jwtService)
        {
            this._jwtService = jwtService;
            this._googleService = googleService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            GoogleLogInUserCommand request,
            CancellationToken cancellationToken
        )
        {
            string? accessToken = await this._googleService.GetAccessTokenAsync(
                request.Code,
                cancellationToken
            );
            if (accessToken is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            UserInfo? userInfo = await this._googleService.GetUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (userInfo is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            userInfo.Code = request.Code;

            Result<AccessTokenResponse> result = await this._jwtService.AuthenticateWithGoogleAsync(
                userInfo,
                cancellationToken
            );
            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return result;
        }
    }
}
