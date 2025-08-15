using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipAddedDomainEvent(Client client, Membership membership)
                : IDomainEvent
    {
        public Client Client { get; } = client;
        public Membership Membership { get; } = membership;
        public DateTime OccurredOn => DateTime.UtcNow;

    }
}
