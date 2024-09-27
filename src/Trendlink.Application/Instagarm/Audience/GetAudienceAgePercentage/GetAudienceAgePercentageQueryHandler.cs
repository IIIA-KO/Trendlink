using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceAgePercentage
{
    internal sealed class GetAudienceAgePercentageQueryHandler
        : IQueryHandler<GetAudienceAgePercentageQuery, AudienceAgeStatistics>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _keycloakService;

        public GetAudienceAgePercentageQueryHandler(
            IUserRepository userRepository,
            IInstagramService instagramService,
            IKeycloakService keycloakService
        )
        {
            this._userRepository = userRepository;
            this._instagramService = instagramService;
            this._keycloakService = keycloakService;
        }

        public async Task<Result<AudienceAgeStatistics>> Handle(
            GetAudienceAgePercentageQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<AudienceAgeStatistics>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<AudienceAgeStatistics>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetAudienceAgePercentage(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                cancellationToken
            );
        }
    }
}
