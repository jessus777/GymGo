using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipCreatedDomainEvent(Membership membership)
                : IDomainEvent
    {
        public Membership Membership { get; } = membership;

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
