using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Audience.GetAudienceAgePercentage
{
    public sealed record GetAudienceAgePercentageQuery(UserId UserId)
        : ICachedQuery<AudienceAgeStatistics>
    {
        public string CacheKey => $"audience-age-{this.UserId.Value}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
