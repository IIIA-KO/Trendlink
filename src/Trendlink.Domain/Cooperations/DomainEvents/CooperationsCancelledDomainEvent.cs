using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationsCancelledDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
