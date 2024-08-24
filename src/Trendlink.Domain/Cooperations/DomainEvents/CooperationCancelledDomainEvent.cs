namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationCancelledDomainEvent(CooperationId CooperationId)
        : ICooperationDomainEvent;
}
