using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.UnitTests.Infrastructure
{
    public abstract class BaseTest
    {
        public static TEvent AssertDomainEventWasPublished<TEvent, TEntityId>(Entity<TEntityId> entity)
            where TEvent : IDomainEvent
            where TEntityId : class
        {
            TEvent? domainEvent = entity.GetDomainEvents().OfType<TEvent>().SingleOrDefault() 
                ?? throw new Exception($"{typeof(TEvent).Name} was not published");

            return domainEvent;
        }
    }
}
