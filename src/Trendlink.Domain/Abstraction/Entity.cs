namespace Trendlink.Domain.Abstraction
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id)
        {
            this.Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }

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
