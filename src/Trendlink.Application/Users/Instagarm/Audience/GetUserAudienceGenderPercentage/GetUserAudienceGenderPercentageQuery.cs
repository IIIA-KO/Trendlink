using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage
{
    public sealed record GetUserAudienceGenderPercentageQuery(UserId UserId)
        : ICachedQuery<AudienceGenderStatistics>
    {
        public string CacheKey => $"audience-gender-{this.UserId.Value}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
