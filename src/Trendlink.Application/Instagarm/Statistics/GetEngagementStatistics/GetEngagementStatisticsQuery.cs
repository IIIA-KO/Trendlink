using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics
{
    public sealed record GetEngagementStatisticsQuery(
        UserId UserId,
        StatisticsPeriod StatisticsPeriod
    ) : ICachedQuery<EngagementStatistics>
    {
        public string CacheKey => $"engagement-{this.UserId.Value}-{(int)this.StatisticsPeriod}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
