using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Application.Users.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetEngagementStatistics
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
