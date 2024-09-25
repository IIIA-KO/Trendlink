using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics
{
    internal sealed class GetOverViewStatisticsQueryHandler
        : IQueryHandler<GetOverviewStatisticsQuery, OverviewStatistics>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;
        private readonly IInstagramService _instagramService;

        public GetOverViewStatisticsQueryHandler(
            IUserRepository userRepository,
            IKeycloakService keycloakService,
            IInstagramService instagramService
        )
        {
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
            this._instagramService = instagramService;
        }

        public async Task<Result<OverviewStatistics>> Handle(
            GetOverviewStatisticsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<OverviewStatistics>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<OverviewStatistics>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetOverviewStatistics(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                request.StatisticsPeriod,
                cancellationToken
            );
        }
    }
}
