using GymGo.Application.Contracts.Identity;
using GymGo.Identity.Contexts;
using GymGo.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Identity.Repositories
{
    public class UnitOfWorkIdentityFactory
        : IUnitOfWorkIdentityFactory
    {

        private readonly IDbContextFactory<IdentityDbContext> _identityDbFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWorkIdentityFactory(
            IDbContextFactory<IdentityDbContext> identityDbFactory
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            )
        {
            _identityDbFactory = identityDbFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IIdentityUnitOfWork Create()
        {
            var dbContext = _identityDbFactory.CreateDbContext();

            return new IdentityUnitOfWork(dbContext, _userManager, _roleManager);
        }
    }
}
