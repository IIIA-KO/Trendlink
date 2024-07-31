namespace Trendlink.Domain.Cooperations
{
    public sealed record CooperationId(Guid Value)
    {
        public static CooperationId New() => new(Guid.NewGuid());
    }
}
