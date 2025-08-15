using MediatR;

namespace GymGo.Domain.Domain_Events
{
    public interface IDomainEvent
        : INotification
    {
        DateTime OccurredOn { get; }
    }
}
