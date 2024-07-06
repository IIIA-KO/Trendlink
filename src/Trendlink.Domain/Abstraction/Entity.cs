namespace Trendlink.Domain.Abstraction
{
    public abstract class Entity<TEntityId> : IEntity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected Entity(TEntityId id)
        {
            this.Id = id;
        }

        protected Entity() { }

        public TEntityId Id { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return this._domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            this._domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            this._domainEvents.Add(domainEvent);
        }
    }
}
