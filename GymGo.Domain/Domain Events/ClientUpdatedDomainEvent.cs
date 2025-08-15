using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class ClientUpdatedDomainEvent(Client client)
                : IDomainEvent
    {
        public Client Client { get; } = client ?? throw new ArgumentNullException(nameof(client));

        public DateTime OccurredOn => DateTime.UtcNow;
    }

}
