using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.LinkInstagram
{
    internal sealed class LinkInstagramCommandHandler : ICommandHandler<LinkInstagramCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LinkInstagramCommandHandler(
            IUserRepository userRepository,
            IUserContext userContext,
            IInstagramService instagramService,
            IKeycloakService jwtService,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._userContext = userContext;
            this._instagramService = instagramService;
            this._jwtService = jwtService;
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
                await this._jwtService.IsExternalIdentityProviderAccountLinkedAsync(
                    user!.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (isInstagramLinked)
            {
                return Result.Failure(UserErrors.InstagramAccountAlreadyLinked);
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

            user.Bio = new Bio(instagramUserInfo.BusinessDiscovery.Biography);
            user.SetProfilePicture(new Uri(instagramUserInfo.BusinessDiscovery.ProfilePictureUrl));

            InstagramAccount instagramAccount = instagramUserInfo.CreateInstagramAccount(user.Id);
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
