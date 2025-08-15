using GymGo.Domain.Aggregates;
using GymGo.Domain.Domain_Events;

namespace GymGo.Domain.Entities
{
    public sealed class Client
        : AggregateRoot
    {
        private readonly List<Membership> _memberships = new();
        private readonly List<ClientHistory> _history = new();

        public IReadOnlyCollection<Membership> Memberships => _memberships.AsReadOnly();
        public IReadOnlyCollection<ClientHistory> History => _history.AsReadOnly();
        private Client(
            Guid id,
            string name,
            string email,
            string phoneNumber,
            string address,
            string urlImage
            )
            : base(id)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            UrlImage = urlImage;

            IsActive = true;        // Evento de dominio: cliente creado
            RaiseDomainEvent(new ClientCreatedDomainEvent(this));
        }

        public string Name { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = null!;
        public string UrlImage { get; set; } = null;
        public bool IsActive { get; set; }

        // Método de fábrica para crear un cliente (opcional, recomendado)
        public static Client Create(
            string name,
            string email,
            string phoneNumber,
            string address = null,
            string urlImage = null
            )
        {
            return new Client(Guid.NewGuid(), name, email, phoneNumber, address, urlImage);
        }

        // Actualizar datos del cliente
        public void Update(string name, string email, string phoneNumber, string address, string urlImage)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            UrlImage = urlImage;

            RaiseDomainEvent(new ClientUpdatedDomainEvent(this));
        }

        // Eliminar (desactivar) cliente
        //public void Delete(string? reason = null)
        //{
        //    IsActive = false;
        //    RaiseDomainEvent(new ClientDeletedDomainEvent(this, reason));
        //}

        // Agregar membresía
        public void AddMembership(Membership membership)
        {
            _memberships.Add(membership);
            RaiseDomainEvent(new MembershipAddedDomainEvent(this, membership));
        }

        // Agregar histórico
        public void AddHistory(ClientHistory history)
        {
            _history.Add(history);
            RaiseDomainEvent(new ClientHistoryAddedDomainEvent(this, history));
        }

    }
}
