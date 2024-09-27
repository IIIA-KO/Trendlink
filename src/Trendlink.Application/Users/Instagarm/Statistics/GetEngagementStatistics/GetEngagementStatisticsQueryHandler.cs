using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetEngagementStatistics
{
    internal sealed class GetEngagementStatisticsQueryHandler
        : IQueryHandler<GetEngagementStatisticsQuery, EngagementStatistics>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;
        private readonly IInstagramService _instagramService;

        public GetEngagementStatisticsQueryHandler(
            IUserRepository userRepository,
            IKeycloakService keycloakService,
            IInstagramService instagramService
        )
        {
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
            this._instagramService = instagramService;
        }

        public async Task<Result<EngagementStatistics>> Handle(
            GetEngagementStatisticsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<EngagementStatistics>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<EngagementStatistics>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetEngagementStatistics(
                user.InstagramAccount!.Metadata.FollowersCount,
                new InstagramPeriodRequest(
                    user.Token!.AccessToken,
                    user.InstagramAccount!.Metadata.Id,
                    request.StatisticsPeriod
                ),
                cancellationToken
            );
        }
    }
}
