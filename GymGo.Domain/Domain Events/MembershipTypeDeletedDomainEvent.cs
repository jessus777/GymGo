using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipTypeDeletedDomainEvent
        : IDomainEvent
    {
        public MembershipTypeDeletedDomainEvent(MembershipType membershipType)
        {
            MembershipType = membershipType;
        }

        public MembershipType MembershipType { get; set; }

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
