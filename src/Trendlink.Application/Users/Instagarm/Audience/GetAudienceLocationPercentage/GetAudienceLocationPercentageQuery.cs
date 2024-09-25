using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage
{
    public sealed record GetAudienceLocationPercentageQuery(
        UserId UserId,
        LocationType LocationType
    ) : ICachedQuery<AudienceLocationStatistics>
    {
        public string CacheKey => $"audience-location-{this.UserId.Value}-{(int)this.LocationType}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
