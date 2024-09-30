using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio
{
    public sealed record GetAudienceLocationRatioQuery(UserId UserId, LocationType LocationType)
        : ICachedQuery<LocationRatioResponse>
    {
        public string CacheKey => $"audience-location-{this.UserId.Value}-{(int)this.LocationType}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
