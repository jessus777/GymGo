using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Commands.CreateMembershipType
{
    public sealed class CreateMembershipTypeCommand
        : ApplicationRequest<MembershipTypeDto>
    {
        public CreateMembershipTypeCommand(string name, string description, decimal price, int durationInDays)
        {
            Name = name;
            Description = description;
            Price = price;
            DurationInDays = durationInDays;
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int DurationInDays { get; private set; }
    }
}
