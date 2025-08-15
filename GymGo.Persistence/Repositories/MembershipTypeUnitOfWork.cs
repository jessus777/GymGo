using GymGo.Application.Contracts.Persistence;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Helpers.SqlFileLoader;
using System.Data;

namespace GymGo.Persistence.Repositories
{
    public class MembershipTypeUnitOfWork
        : IMembershipTypeUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbConnection _connection;
        private readonly ISqlFileLoader _sqlFileLoader;

        public MembershipTypeUnitOfWork(
            ApplicationDbContext dbContext,
            IDbConnection connection,
            ISqlFileLoader sqlFileLoader
        )
        {
            _dbContext = dbContext;
            _connection = connection;
            _sqlFileLoader = sqlFileLoader;
        }

        private IMembershipTypeRepositoryAsync? _membershipTypeRepositoryAsync;
        public IMembershipTypeRepositoryAsync MembershipTypeRepositoryAsync =>
            _membershipTypeRepositoryAsync ??= new MembershipTypeRepositoryAsync(_dbContext);

        private IMembershipTypeDapperQueriesRepositoryAsync? _membershipTypeDapperQueriesRepositoryAsync;
        public IMembershipTypeDapperQueriesRepositoryAsync MembershipTypeDapperQueriesRepositoryAsync =>
            _membershipTypeDapperQueriesRepositoryAsync ??= new MembershipTypeDapperQueriesRepositoryAsync(_connection, _sqlFileLoader);

    }
}
