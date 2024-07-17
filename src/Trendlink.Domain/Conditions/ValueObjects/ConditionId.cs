namespace Trendlink.Domain.Conditions.ValueObjects
{
    public sealed record ConditionId(Guid Value)
    {
        public static ConditionId New() => new(Guid.NewGuid());
    }
}
