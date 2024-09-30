using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceReachRatio
{
    public sealed record GetAudienceReachRatioQuery(
        UserId UserId,
        StatisticsPeriod StatisticsPeriod
    ) : ICachedQuery<ReachRatio>
    {
        public string CacheKey =>
            $"audience-reach-{this.UserId.Value}-{(int)this.StatisticsPeriod}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
