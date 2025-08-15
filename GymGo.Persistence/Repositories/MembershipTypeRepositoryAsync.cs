using GymGo.Application.Contracts.Persistence;
using GymGo.Domain.Entities;
using GymGo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GymGo.Persistence.Repositories
{
    public class MembershipTypeRepositoryAsync
        : RepositoryAsync<MembershipType>, IMembershipTypeRepositoryAsync
    {
        public MembershipTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<MembershipType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MembershipTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                   mt => mt.Name.ToLower() == name.ToLower(),
                   cancellationToken
                );
        }
    }
}
