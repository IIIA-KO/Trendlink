using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Users.DomainEvents
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
}
