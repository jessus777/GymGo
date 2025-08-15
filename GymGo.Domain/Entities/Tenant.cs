using GymGo.Domain.Aggregates;
using GymGo.Domain.Common;

namespace GymGo.Domain.Entities
{
    public sealed class Tenant
        : AggregateRoot, IAuditable
    {
        //private readonly List<ApplicationUser> _users = [];
        //public IReadOnlyCollection<ApplicationUser> Users => _users.AsReadOnly();
        private Tenant(Guid id, string name, string email, string phone, string address)
            : base(id)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            IsActive = true; // Por defecto, un tenant es activo al ser creado
            IsDeleted = false;
        }

        public string Name { get; private set; } = null!;
        public string Subdomain { get; private set; } = null;
        public string Email { get; private set; } = null!;
        public string Phone { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public AuditableEntity Auditoria { get; set; } = new();

        public static Tenant Create(string name, string email, string phone, string address)
        {
            var tenant = new Tenant(Guid.NewGuid(), name, email, phone, address);
            return tenant;
        }

        //public void AddUser(ApplicationUser user)
        //{


        //    _users.Add(user);

        //    // Disparamos evento de dominio
        //    //RaiseDomainEvent(new UserAddedToTenantDomainEvent(Id, user.Id));
        //}
    }
}
