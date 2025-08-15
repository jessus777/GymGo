using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipTypeDeactivatedDomainEvent
        : IDomainEvent
    {
        public MembershipTypeDeactivatedDomainEvent(MembershipType membershipType)
        {
            MembershipType = membershipType ?? throw new ArgumentNullException(nameof(membershipType));
        }
        public MembershipType MembershipType
        {
            get;
        }

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
