using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public sealed record CooperationsCancelledDomainEvent(Cooperation Cooperation) : IDomainEvent;
}
