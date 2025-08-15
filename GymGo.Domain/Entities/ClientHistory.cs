using GymGo.Domain.Aggregates;
using GymGo.Domain.Domain_Events;
using GymGo.Domain.Enums;

namespace GymGo.Domain.Entities
{
    public class ClientHistory
        : AggregateRoot
    {
        private ClientHistory(Guid id, Guid clientId, ClientHistoryActionType actionType, string description)
            : base(id)
        {
            ClientId = clientId;
            ActionType = actionType;
            Description = description;
            Timestamp = DateTime.UtcNow; 

            RaiseDomainEvent(new ClientHistoryCreatedDomainEvent(this));

        }

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public ClientHistoryActionType ActionType { get; set; }
        public string Description { get; set; } = null;
        public DateTime Timestamp { get; set; }

        // Método de fábrica recomendado
        public static ClientHistory Create(Guid clientId, ClientHistoryActionType actionType, string description)
        {
            return new ClientHistory(Guid.NewGuid(), clientId, actionType, description);
        }
    }
}
