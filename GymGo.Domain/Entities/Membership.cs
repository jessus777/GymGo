using GymGo.Domain.Aggregates;
using GymGo.Domain.Domain_Events;

namespace GymGo.Domain.Entities
{
    public class Membership
        : AggregateRoot
    {
        private readonly List<ClientHistory> _history = new();
        public IReadOnlyCollection<ClientHistory> History => _history.AsReadOnly();
        private Membership(
            Guid id,
            Guid clientId,
            Guid membershipTypeId,
            DateTime startDate,
            DateTime endDate
            )
            : base(id)
        {
            ClientId = clientId;
            MembershipTypeId = membershipTypeId;
            StartDate = startDate;
            EndDate = endDate;
            // Raise domain event for membership creation
            RaiseDomainEvent(new MembershipCreatedDomainEvent(this));
        }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid MembershipTypeId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => EndDate >= DateTime.UtcNow;

        // Método de fábrica recomendado
        public static Membership Create(
            Guid clientId,
            Guid membershipTypeId,
            DateTime startDate,
            DateTime endDate)
        {
            return new Membership(Guid.NewGuid(), clientId, membershipTypeId, startDate, endDate);
        }

        // Activar membresía (ejemplo de lógica de negocio)
        public void Activate()
        {
            // Aquí podrías agregar lógica de activación si aplica
            RaiseDomainEvent(new MembershipActivatedDomainEvent(this));
        }

        // Cancelar membresía
        public void Cancel()
        {
            // Aquí podrías agregar lógica de cancelación si aplica
            RaiseDomainEvent(new MembershipCanceledDomainEvent(this));
        }

        // Renovar membresía
        public void Renew(DateTime newEndDate)
        {
            EndDate = newEndDate;
            RaiseDomainEvent(new MembershipRenewedDomainEvent(this));
        }

        // Agregar histórico
        public void AddHistory(ClientHistory history)
        {
            _history.Add(history);
            RaiseDomainEvent(new ClientHistoryAddedDomainEvent(Client, history));
        }

    }
}