using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationDoneDomainEvent(CooperationId CooperationId) : IDomainEvent;
}
