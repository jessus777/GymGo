using GymGo.Application.Contracts.Identity;
using GymGo.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace GymGo.Identity.Repositories
{
    public class RoleRepositoryAsync
        : IRoleRepositoryAsync
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepositoryAsync(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}
