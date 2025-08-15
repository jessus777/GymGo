using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class ClientCreatedDomainEvent
        : IDomainEvent
    {
        public Client Client { get; }

        public DateTime OccurredOn => DateTime.UtcNow;

        public ClientCreatedDomainEvent(Client client) => Client = client;
    }
}
