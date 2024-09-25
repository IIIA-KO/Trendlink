namespace Trendlink.Application.Users.Instagarm.Posts.GetPosts
{
    public sealed class PostsResponse
    {
        public List<InstagramPost> Posts { get; init; }

        public InstagramPagingResponse Paging { get; init; }
    }

    public sealed class InstagramPost
    {
        public string Id { get; set; }

        public string MediaType { get; set; }

        public string MediaUrl { get; set; }

        public string Permalink { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime Timestamp { get; set; }

        public List<InstagramInsight> Insights { get; set; }
    }

    public class InstagramInsight
    {
        public string Name { get; set; }

        public int? Value { get; set; }
    }

    public class InstagramPagingResponse
    {
        public string? Before { get; set; }

        public string? After { get; set; }

        public string? NextCursor { get; set; }

        public string? PreviousCursor { get; set; }
    }
}
