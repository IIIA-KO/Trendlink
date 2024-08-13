using System.Globalization;
using Trendlink.Application.Abstractions.Authentication;
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
            (string? accessToken, long? instagramUserId) =
                await this._instagramService.GetAccessTokenAsync(request.Code, cancellationToken);
            if (accessToken is null || instagramUserId is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            InstagramUserInfo? userInfo = await this._instagramService.GetUserInfoAsync(
                accessToken,
                instagramUserId.Value,
                cancellationToken
            );
            if (userInfo is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            bool isLinked = await this._jwtService.LinkInstagramAccountToKeycloakUserAsync(
                this._userContext.IdentityId,
                instagramUserId.Value.ToString(CultureInfo.InvariantCulture),
                userInfo.Username,
                cancellationToken
            );
            if (!isLinked)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            return Result.Success();
        }
    }
}
