using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class ClientHistoryAddedDomainEvent(Client client, ClientHistory history)
        : IDomainEvent
    {
        public Client Client { get; } = client;
        public ClientHistory History { get; } = history;

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
