using GymGo.Domain.Common;
using GymGo.Domain.Domain_Events;

namespace GymGo.Domain.Aggregates
{
    public abstract class AggregateRoot
        : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        void IAggregateRoot.ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
    }
}
