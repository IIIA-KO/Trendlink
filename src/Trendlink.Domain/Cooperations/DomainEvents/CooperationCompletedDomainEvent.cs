using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationCompletedDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
