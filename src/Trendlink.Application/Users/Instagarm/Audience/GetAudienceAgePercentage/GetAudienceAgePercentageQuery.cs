using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
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
