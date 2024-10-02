using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Review.DomainEvents
{
    public sealed record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;
}
