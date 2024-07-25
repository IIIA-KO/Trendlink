using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationRejectedDomainEvent(CooperationId CooperationId) : IDomainEvent;
}
