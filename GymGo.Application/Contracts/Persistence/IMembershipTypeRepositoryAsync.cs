using GymGo.Application.Dtos;
using GymGo.Domain.Entities;

namespace GymGo.Application.Contracts.Persistence
{
    public interface IMembershipTypeRepositoryAsync
        : IRepositoryAsync<MembershipType>
    {
        Task<MembershipType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
