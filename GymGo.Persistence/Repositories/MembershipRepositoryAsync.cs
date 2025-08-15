using GymGo.Application.Contracts.Persistence;
using GymGo.Domain.Entities;
using GymGo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Persistence.Repositories
{
    public class MembershipRepositoryAsync
        : RepositoryAsync<Membership>, IMembershipRepositoryAsync
    {
        public MembershipRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
