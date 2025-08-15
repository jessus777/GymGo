using GymGo.Application.Contracts;
using GymGo.Application.Contracts.Identity;
using GymGo.Application.Contracts.Persistence;
using GymGo.Identity.Contexts;
using GymGo.Identity.Repositories;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Helpers.SqlFileLoader;
using GymGo.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Shared
{
    public class AppUnitOfWorkFactory
        : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IDbContextFactory<IdentityDbContext> _identityDbFactory;
        private readonly IMediator _mediator;
        private readonly ISqlFileLoader _sqlFileLoader;

        public AppUnitOfWorkFactory(
            IDbContextFactory<ApplicationDbContext> dbContextFactory, 
            IDbContextFactory<IdentityDbContext> identityDbFactory, 
            IMediator mediator, 
            ISqlFileLoader sqlFileLoader
            )
        {
            _dbContextFactory = dbContextFactory;
            _identityDbFactory = identityDbFactory;
            _mediator = mediator;
            _sqlFileLoader = sqlFileLoader;
        }

        public IUnitOfWork Create()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            //return new UnitOfWork(dbContext, _dbContextFactory, _mediator, _sqlFileLoader);
            return new UnitOfWork(dbContext, _mediator, _sqlFileLoader);

        }

        public IIdentityUnitOfWork CreateIdentity()
        {
            var dbContext = _identityDbFactory.CreateDbContext();

            return new IdentityUnitOfWork(dbContext);
        }
    }
}
