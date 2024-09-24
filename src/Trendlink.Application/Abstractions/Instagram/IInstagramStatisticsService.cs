using Trendlink.Application.Users.Instagarm.GetTableStatistics;
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
    }
}
