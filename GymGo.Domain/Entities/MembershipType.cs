using GymGo.Domain.Aggregates;
using GymGo.Domain.Common;
using GymGo.Domain.Domain_Events;

namespace GymGo.Domain.Entities
{
    public class MembershipType
        : AggregateRoot, IAuditable
    {
        private readonly List<Membership> _memberships = new();

        public MembershipType()
            : base(Guid.Empty)
        {

        }
        private MembershipType(Guid id, string name, string description, decimal price, int durationInDays)
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            DurationInDays = durationInDays;
            IsActive = true; // Por defecto, una membresía es activa al ser creada
            IsDeleted = false;
            RaiseDomainEvent(new MembershipTypeCreatedDomainEvent(this));
        }

        public IReadOnlyCollection<Membership> Memberships => _memberships.AsReadOnly();

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public AuditableEntity Auditoria { get; set; } = new();

        // Método de fábrica recomendado
        public static MembershipType Create(string name, string description, decimal price, int durationInDays)
        {

            var membershipType = new MembershipType(Guid.NewGuid(), name, description, price, durationInDays);
            return membershipType;
        }

        public void Activate()
        {
            IsActive = true;
            RaiseDomainEvent(new MembershipTypeActivatedDomainEvent(this));
        }

        public void Deactivate()
        {
            IsActive = false;
            RaiseDomainEvent(new MembershipTypeDeactivatedDomainEvent(this));
        }
        public void Delete()
        {

            if (IsDeleted) return;

            Deactivate();
            IsDeleted = true;
            RaiseDomainEvent(new MembershipTypeDeletedDomainEvent(this));
        }

        public void UpdateDetails(string name, string description, decimal price, int durationInDays)
        {
            Name = name;
            Description = description;
            Price = price;
            DurationInDays = durationInDays;
            RaiseDomainEvent(new MembershipTypeUpdatedDomainEvent(this));
        }
    }
}


