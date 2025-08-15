using GymGo.Application.Contracts.Identity;
using GymGo.Domain.Entities;
using GymGo.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Identity.Repositories
{
    public class UserRepositoryAsync
        : IUserRepositoryAsync
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepositoryAsync(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task CreateAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var identityUsers = _userManager.Users
                .AsNoTracking()
                .Select(u => new User(Guid.Parse(u.Id), u.UserName!, u.Email!));
            return await identityUsers.ToListAsync(cancellationToken);
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var identityUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == id.ToString(), cancellationToken);

            return identityUser is null
                ? null
                : new User(Guid.Parse(identityUser.Id), identityUser.UserName!, identityUser.Email!);
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
