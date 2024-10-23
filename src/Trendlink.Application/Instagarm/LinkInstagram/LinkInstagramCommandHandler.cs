using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.Instagarm.LinkInstagram
{
    internal sealed class LinkInstagramCommandHandler : ICommandHandler<LinkInstagramCommand>
    {
        private const string ProviderName = "instagram";

        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _keycloakService;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LinkInstagramCommandHandler(
            IUserRepository userRepository,
            IUserContext userContext,
            IInstagramService instagramService,
            IKeycloakService keycloakService,
            IUserTokenRepository userTokenRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._userContext = userContext;
            this._instagramService = instagramService;
            this._keycloakService = keycloakService;
            this._userTokenRepository = userTokenRepository;
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

            Result<FacebookTokenResponse>? facebookTokenResult =
                await this._instagramService.GetAccessTokenAsync(request.Code, cancellationToken);
            if (facebookTokenResult.IsFailure)
            {
                return Result.Failure(facebookTokenResult.Error);
            }

            Result<InstagramAccount> instagramAccountResult =
                await this._instagramService.GetInstagramAccountAsync(
                    user.Id,
                    facebookTokenResult.Value.AccessToken,
                    cancellationToken
                );
            if (instagramAccountResult.IsFailure)
            {
                return Result.Failure(instagramAccountResult.Error);
            }
            InstagramAccount instagramAccount = instagramAccountResult.Value;

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

            user.LinkInstagramAccount(instagramAccount);

            Result<UserToken> userTokenResult = UserToken.Create(
                user.Id,
                facebookTokenResult.Value.AccessToken,
                facebookTokenResult.Value.ExpiresAtUtc
            );
            if (userTokenResult.IsFailure)
            {
                return Result.Failure(userTokenResult.Error);
            }
            this._userTokenRepository.Add(userTokenResult.Value);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
