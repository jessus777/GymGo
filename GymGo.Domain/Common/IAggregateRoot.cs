using GymGo.Domain.Domain_Events;

namespace GymGo.Domain.Common
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void RaiseDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
