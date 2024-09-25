using Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramStatisticsService
    {
        Task<Result<TableStatistics>> GetTableStatistics(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        );

        Task<Result<OverviewStatistics>> GetOverviewStatistics(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        );
    }
}
