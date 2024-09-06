namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationPendedDomainEvent(CooperationId CooperationId)
        : ICooperationDomainEvent;
}
