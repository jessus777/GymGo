using GymGo.Application.Contracts.Persistence;
using GymGo.Domain.Entities;
using GymGo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Persistence.Repositories
{
    public class ClientHistoryRepositoryAsync
        : RepositoryAsync<ClientHistory>, IClientHistoryRepositoryAsync
    {
        public ClientHistoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
