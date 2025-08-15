using GymGo.Application.Contracts.Persistence;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Helpers.SqlFileLoader;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Persistence.Repositories
{
    public class UnitOfWorkFactory
        : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMediator _mediator;
        private readonly ISqlFileLoader _sqlFileLoader;

        public UnitOfWorkFactory(
            IDbContextFactory<ApplicationDbContext> dbContextFactory
            , IMediator mediator
            , ISqlFileLoader sqlFileLoader
            )
        {
            _dbContextFactory = dbContextFactory;
            _mediator = mediator;
            _sqlFileLoader = sqlFileLoader;
        }

        public IUnitOfWork Create()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return new UnitOfWork(dbContext, _mediator, _sqlFileLoader);
        }
    }
}
