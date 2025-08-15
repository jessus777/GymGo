using GymGo.Application.Contracts.Persistence;
using GymGo.Domain.Entities;
using GymGo.Persistence.Contexts;

namespace GymGo.Persistence.Repositories
{
    public class ClientRepositoryAsync
        : RepositoryAsync<Client>, IClientRepositoryAsync
    {
        public ClientRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
