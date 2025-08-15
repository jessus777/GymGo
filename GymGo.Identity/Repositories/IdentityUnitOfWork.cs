using GymGo.Application.Contracts.Identity;
using GymGo.Identity.Contexts;
using GymGo.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace GymGo.Identity.Repositories
{
    public class IdentityUnitOfWork
        : IIdentityUnitOfWork, IDisposable
    {
        private readonly IdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool _disposed = false;


        public IdentityUnitOfWork(
            IdentityDbContext context
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private IUserRepositoryAsync? _userRepositoryAsync;
        public IUserRepositoryAsync UserRepositoryAsync =>
            _userRepositoryAsync ??= new UserRepositoryAsync(_userManager);

        private IRoleRepositoryAsync? _roleRepositoryAsync;
        public IRoleRepositoryAsync RoleRepositoryAsync =>
            _roleRepositoryAsync ??= new RoleRepositoryAsync(_roleManager);

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // No llamar de nuevo al finalizer        }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //_currentTransaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
