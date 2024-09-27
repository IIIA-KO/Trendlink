using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetInteractionStatistics
{
    internal sealed class GetInteractionStatisticsQueryHandler
        : IQueryHandler<GetInteractionStatisticsQuery, InteractionStatistics>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;
        private readonly IInstagramService _instagramService;

        public GetInteractionStatisticsQueryHandler(
            IUserRepository userRepository,
            IKeycloakService keycloakService,
            IInstagramService instagramService
        )
        {
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
            this._instagramService = instagramService;
        }

        public async Task<Result<InteractionStatistics>> Handle(
            GetInteractionStatisticsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<InteractionStatistics>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<InteractionStatistics>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetInteractionStatistics(
                user.InstagramAccount!.AdvertisementAccountId.Value,
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
