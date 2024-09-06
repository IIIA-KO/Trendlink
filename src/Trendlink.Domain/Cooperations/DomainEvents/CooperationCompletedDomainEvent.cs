namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationCompletedDomainEvent(CooperationId CooperationId)
        : ICooperationDomainEvent;
}
