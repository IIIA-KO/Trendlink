using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.DomainEvents
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
}
