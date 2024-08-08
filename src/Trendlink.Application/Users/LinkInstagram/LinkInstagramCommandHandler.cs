using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.LinkInstagram
{
    internal sealed class LinkInstagramCommandHandler : ICommandHandler<LinkInstagramCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IInstagramService _instagramService;
        private readonly IJwtService _jwtService;

        public LinkInstagramCommandHandler(
            IUserContext userContext,
            IInstagramService instagramService,
            IJwtService jwtService
        )
        {
            this._userContext = userContext;
            this._instagramService = instagramService;
            this._jwtService = jwtService;
        }

        public async Task<Result> Handle(
            LinkInstagramCommand request,
            CancellationToken cancellationToken
        )
        {
            /*
             * Implementation
             */

            UserId userId = this._userContext.UserId;

            AccessTokenResponse accessToken = await this._instagramService.GetAccessTokenAsync(
                request.Code,
                cancellationToken
            );
            if (accessToken == null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            InstagramUserInfo? userInfo = await this._instagramService.GetUserInfoAsync(
                accessToken.AccessToken,
                cancellationToken
            );
            if (userInfo is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            bool isUpdated = await this._jwtService.UpdateUserAttributesAsync(
                userId.Value,
                new Dictionary<string, string>
                {
                    { "instagram_access_token", accessToken.AccessToken },
                    { "instagram_refresh_token", accessToken.RefreshToken },
                    { "instagram_username", userInfo.Username },
                    {
                        "access_token_expiry",
                        DateTime.UtcNow.AddSeconds(accessToken.ExpiresIn).ToString("o")
                    }
                },
                cancellationToken
            );

            if (!isUpdated)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            return Result.Success();
        }
    }
}
