namespace GymGo.Application.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
