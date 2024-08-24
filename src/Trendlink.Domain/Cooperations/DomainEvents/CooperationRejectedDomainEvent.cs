namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationRejectedDomainEvent(CooperationId CooperationId)
        : ICooperationDomainEvent;
}
