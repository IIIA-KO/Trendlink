using System.Globalization;
using System.Net.Http;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

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
            bool isAlreadyLinked = await this._jwtService.IsInstagramAccountLinkedAsync(
                this._userContext.IdentityId,
                cancellationToken
            );
            if (isAlreadyLinked)
            {
                return Result.Failure(UserErrors.InstagramAccountAlreadyLinked);
            }

            FacebookTokenResponse? facebookAccessToken =
                await this._instagramService.GetAccessTokenAsync(request.Code, cancellationToken);
            if (facebookAccessToken is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            Result<InstagramUserInfo> userInfoResult =
                await this._instagramService.GetUserInfoAsync(
                    facebookAccessToken.AccessToken,
                    cancellationToken
                );
            if (userInfoResult.IsFailure)
            {
                return Result.Failure(userInfoResult.Error);
            }

            InstagramUserInfo userInfo = userInfoResult.Value;

            Result result =
                await this._jwtService.LinkExternalIdentityProviderAccountToKeycloakUserAsync(
                    this._userContext.IdentityId,
                    "instagram",
                    userInfo.Id.ToString(CultureInfo.InvariantCulture),
                    userInfo.BusinessDiscovery.Username,
                    cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            return Result.Success();
        }
    }
}
