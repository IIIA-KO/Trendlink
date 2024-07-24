using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationConfirmedDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
