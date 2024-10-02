namespace Trendlink.Domain.Review
{
    public sealed record ReviewId(Guid Value)
    {
        public static ReviewId New() => new(Guid.NewGuid());
    }
}
