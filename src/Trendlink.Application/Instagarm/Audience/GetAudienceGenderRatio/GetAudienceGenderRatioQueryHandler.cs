using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio
{
    internal sealed class GetAudienceGenderRatioQueryHandler
        : IQueryHandler<GetAudienceGenderRatioQuery, GenderRatioResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _keycloakService;

        public GetAudienceGenderRatioQueryHandler(
            IUserRepository userRepository,
            IInstagramService instagramService,
            IKeycloakService keycloakService
        )
        {
            this._userRepository = userRepository;
            this._instagramService = instagramService;
            this._keycloakService = keycloakService;
        }

        public async Task<Result<GenderRatioResponse>> Handle(
            GetAudienceGenderRatioQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<GenderRatioResponse>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<GenderRatioResponse>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetAudienceGenderPercentage(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                cancellationToken
            );
        }
    }
}
