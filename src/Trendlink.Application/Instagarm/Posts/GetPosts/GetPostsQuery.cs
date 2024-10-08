﻿using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Instagarm.Posts.GetPosts
{
    public sealed record GetPostsQuery(UserId UserId, int Limit, string? CursorType, string? Cursor)
        : ICachedQuery<PostsResponse>
    {
        public string CacheKey =>
            $"posts-{this.UserId.Value}-limit-{this.Limit}-cursorType-{this.CursorType ?? "after"}-cursor-{this.Cursor ?? "null"}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
    }
}
