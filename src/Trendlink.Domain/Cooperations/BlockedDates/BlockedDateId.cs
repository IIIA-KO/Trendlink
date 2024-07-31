namespace Trendlink.Domain.Cooperations.BlockedDates
{
    public sealed record BlockedDateId(Guid Value)
    {
        public static BlockedDateId New() => new(Guid.NewGuid());
    }
}
