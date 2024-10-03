namespace Trendlink.Domain.Reviews
{
    public sealed record ReviewId(Guid Value)
    {
        public static ReviewId New() => new(Guid.NewGuid());
    }
}
