using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipRenewedDomainEvent
        : IDomainEvent
    {
        public MembershipRenewedDomainEvent(Membership membership) 
            => Membership = membership ?? throw new ArgumentNullException(nameof(membership));
        public Membership Membership { get; }

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
