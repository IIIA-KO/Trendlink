using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.Posts.GetPostsTableStatistics
{
    public sealed record GetPostsTableStatisticsQuery(
        UserId UserId,
        StatisticsPeriod StatisticsPeriod
    ) : ICachedQuery<PostsTableStatistics>
    {
        public string CacheKey => $"posts-table-{this.UserId.Value}-{(int)this.StatisticsPeriod}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
    }
}
