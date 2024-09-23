using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.LinkInstagram
{
    internal sealed class LinkInstagramCommandHandler : ICommandHandler<LinkInstagramCommand>
    {
        private const string ProviderName = "instagram";

        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _keycloakService;
        private readonly IUnitOfWork _unitOfWork;

        public LinkInstagramCommandHandler(
            IUserRepository userRepository,
            IUserContext userContext,
            IInstagramService instagramService,
            IKeycloakService keycloakService,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._userContext = userContext;
            this._instagramService = instagramService;
            this._keycloakService = keycloakService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            LinkInstagramCommand request,
            CancellationToken cancellationToken
        )
        {
            User user = await this._userRepository.GetByIdAsync(
                this._userContext.UserId,
                cancellationToken
            );

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user!.IdentityId,
                    ProviderName,
                    cancellationToken
                );
            if (isInstagramLinked)
            {
                return Result.Failure(InstagramAccountErrors.InstagramAccountAlreadyLinked);
            }

            FacebookTokenResponse? facebookToken = await this._instagramService.GetAccessTokenAsync(
                request.Code,
                cancellationToken
            );
            if (facebookToken is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            Result<InstagramUserInfo> instagramUserInfoResult =
                await this._instagramService.GetUserInfoAsync(
                    facebookToken.AccessToken,
                    cancellationToken
                );
            if (instagramUserInfoResult.IsFailure)
            {
                return Result.Failure(instagramUserInfoResult.Error);
            }
            InstagramUserInfo instagramUserInfo = instagramUserInfoResult.Value;

            InstagramAccount instagramAccount = instagramUserInfo.CreateInstagramAccount(user.Id);

            Result linkInstagramResult =
                await this._keycloakService.LinkExternalIdentityProviderAccountToKeycloakUserAsync(
                    user.IdentityId,
                    ProviderName,
                    instagramAccount.Metadata.Id,
                    instagramAccount.Metadata.UserName,
                    cancellationToken
                );
            if (linkInstagramResult.IsFailure)
            {
                return Result.Failure(linkInstagramResult.Error);
            }

            user.LinkInstagramAccount(
                instagramAccount,
                facebookToken.AccessToken,
                facebookToken.ExpiresAtUtc
            );

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
