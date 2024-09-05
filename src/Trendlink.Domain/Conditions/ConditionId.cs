namespace Trendlink.Domain.Conditions
{
    public sealed record ConditionId(Guid Value)
    {
        public static ConditionId New() => new(Guid.NewGuid());
    }
}
