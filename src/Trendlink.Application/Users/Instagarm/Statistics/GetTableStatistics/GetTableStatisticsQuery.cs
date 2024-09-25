using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics
{
    public sealed record GetTableStatisticsQuery(UserId UserId, StatisticsPeriod StatisticsPeriod)
        : ICachedQuery<TableStatistics>
    {
        public string CacheKey => $"table-{this.UserId.Value}-{(int)this.StatisticsPeriod}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
