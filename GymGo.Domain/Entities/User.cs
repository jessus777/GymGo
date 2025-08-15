using GymGo.Domain.Aggregates;
using GymGo.Domain.Common;

namespace GymGo.Domain.Entities
{
    public class User
    : AggregateRoot, IAuditable
    {
        public User(Guid id, string userName, string email)
            : base(id)
        {
            UserName = userName;
            Email = email;
        }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; } = true;
        public AuditableEntity Auditoria { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static User Create(string userName, string email)
            => new(Guid.NewGuid(), userName, email);

        public void Deactivate() => IsActive = false;
    }
}
