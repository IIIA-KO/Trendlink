namespace Trendlink.Domain.Review
{
    public sealed record Comment(string Value)
    {
        public static explicit operator string(Comment comment) => comment.Value;
    }
}
