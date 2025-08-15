using GymGo.Domain.Entities;

namespace GymGo.Domain.Domain_Events
{
    public class ClientHistoryCreatedDomainEvent(ClientHistory clientHistory)
                : IDomainEvent
    {
        public ClientHistory ClientHistory
        {
            get;
        } = clientHistory ?? throw new ArgumentNullException(nameof(clientHistory));

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
