﻿using Trendlink.Application.Abstractions.Caching;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage
{
    public sealed record GetUserAudienceReachPercentageQuery(
        UserId UserId,
        StatisticsPeriod StatisticsPeriod
    ) : ICachedQuery<AudienceReachStatistics>
    {
        public string CacheKey =>
            $"audience-reach-{this.UserId.Value}-{(int)this.StatisticsPeriod}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
