using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipTypeActivatedDomainEvent(MembershipType membershipType)
                : IDomainEvent
    {
        public MembershipType MembershipType
        {
            get;
        } = membershipType ?? throw new ArgumentNullException(nameof(membershipType));

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
