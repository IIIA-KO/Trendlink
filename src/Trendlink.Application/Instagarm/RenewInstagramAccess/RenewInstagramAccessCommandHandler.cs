using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.Instagarm.RenewInstagramAccess
{
    internal sealed class RenewInstagramAccessCommandHandler
        : ICommandHandler<RenewInstagramAccessCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;
        private readonly IInstagramService _instagramService;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IInstagramAccountRepository _instagramAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenewInstagramAccessCommandHandler(
            IUserContext userContext,
            IUserRepository userRepository,
            IKeycloakService keycloakService,
            IInstagramService instagramService,
            IUserTokenRepository userTokenRepository,
            IInstagramAccountRepository instagramAccountRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userContext = userContext;
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
            this._instagramService = instagramService;
            this._userTokenRepository = userTokenRepository;
            this._instagramAccountRepository = instagramAccountRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            RenewInstagramAccessCommand request,
            CancellationToken cancellationToken
        )
        {
            User user = await this._userRepository.GetByIdWithInstagramAccountAsync(
                this._userContext.UserId,
                cancellationToken
            );

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user!.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure(InstagramAccountErrors.InstagramAccountNotLinked);
            }

            Result<FacebookTokenResponse>? facebookTokenResult =
                await this._instagramService.RenewAccessTokenAsync(request.Code, cancellationToken);
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

            if (user.InstagramAccount!.Metadata.Id != instagramAccount.Metadata.Id)
            {
                return Result.Failure(InstagramAccountErrors.WrongInstagramAccount);
            }

            UserToken? userToken = await this._userTokenRepository.GetByUserIdAsync(
                user.Id,
                cancellationToken
            );
            this._userTokenRepository.Remove(userToken!);

            this._instagramAccountRepository.Remove(user.InstagramAccount);
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
