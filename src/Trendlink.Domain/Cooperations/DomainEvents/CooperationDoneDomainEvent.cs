using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationDoneDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
