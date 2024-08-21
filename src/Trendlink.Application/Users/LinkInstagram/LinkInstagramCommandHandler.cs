using System.Globalization;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.LinkInstagram
{
    internal sealed class LinkInstagramCommandHandler : ICommandHandler<LinkInstagramCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IInstagramService _instagramService;
        private readonly IJwtService _jwtService;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LinkInstagramCommandHandler(
            IUserContext userContext,
            IInstagramService instagramService,
            IJwtService jwtService,
            IUserTokenRepository userTokenRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userContext = userContext;
            this._instagramService = instagramService;
            this._jwtService = jwtService;
            this._userTokenRepository = userTokenRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            LinkInstagramCommand request,
            CancellationToken cancellationToken
        )
        {
            bool isAlreadyLinked =
                await this._jwtService.IsExternalIdentityProviderAccountAccountLinkedAsync(
                    this._userContext.IdentityId,
                    "instagram",
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

            var userToken = new UserToken(facebookAccessToken.AccessToken);
            this._userTokenRepository.Add(userToken);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
