using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.GetUserPosts
{
    public sealed record GetUserPostsQuery(UserId UserId, string? CursorType, string? Cursor)
        : ICachedQuery<UserPostsResponse>
    {
        public string CacheKey =>
            $"posts-{this.UserId.Value}-cursorType-{this.CursorType ?? "after"}-cursor-{this.Cursor ?? "null"}";

        public TimeSpan? Expiration =>
            string.IsNullOrEmpty(this.Cursor) || string.IsNullOrEmpty(this.CursorType)
                ? TimeSpan.FromSeconds(30)
                : TimeSpan.FromMinutes(5);
    }
}
