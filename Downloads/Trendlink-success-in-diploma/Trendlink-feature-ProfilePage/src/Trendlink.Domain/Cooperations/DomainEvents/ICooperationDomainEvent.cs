using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.DomainEvents
{
    public interface ICooperationDomainEvent : IDomainEvent
    {
        public CooperationId CooperationId { get; }
    }
}
