namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationConfirmedDomainEvent(CooperationId CooperationId)
        : ICooperationDomainEvent;
}
