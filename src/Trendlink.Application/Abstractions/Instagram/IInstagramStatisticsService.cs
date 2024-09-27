using Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramStatisticsService
    {
        Task<Result<TableStatistics>> GetTableStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<OverviewStatistics>> GetOverviewStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<InteractionStatistics>> GetInteractionsStatistics(
            string instagramAdAccountId,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<EngagementStatistics>> GetEngagementStatistics(
            int followersCount,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
