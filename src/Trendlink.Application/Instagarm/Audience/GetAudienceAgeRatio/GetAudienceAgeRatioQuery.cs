using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio
{
    public sealed record GetAudienceAgeRatioQuery(UserId UserId)
        : ICachedQuery<AudienceAgeRatioResponse>
    {
        public string CacheKey => $"audience-age-{this.UserId.Value}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
