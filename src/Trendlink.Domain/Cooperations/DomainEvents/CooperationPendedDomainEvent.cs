using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationPendedDomainEvent(CooperationId CooperationId) : IDomainEvent;
}
