using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationPendedDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
