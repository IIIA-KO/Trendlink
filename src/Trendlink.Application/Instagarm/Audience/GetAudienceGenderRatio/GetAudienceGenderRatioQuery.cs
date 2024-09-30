using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio
{
    public sealed record GetAudienceGenderRatioQuery(UserId UserId)
        : ICachedQuery<GenderRatioResponse>
    {
        public string CacheKey => $"audience-gender-{this.UserId.Value}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
