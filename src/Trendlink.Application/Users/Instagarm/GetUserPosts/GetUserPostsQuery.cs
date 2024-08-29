using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.GetUserPosts
{
    public sealed record GetUserPostsQuery(UserId UserId)
        : ICachedQuery<IReadOnlyList<PostResponse>>
    {
        public string CacheKey => $"posts-{this.UserId.Value}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
    }
}
