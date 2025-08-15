using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipActivatedDomainEvent(Membership membership)
                : IDomainEvent
    {
        public Membership Membership { get; } = membership ?? throw new ArgumentNullException(nameof(membership));
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
