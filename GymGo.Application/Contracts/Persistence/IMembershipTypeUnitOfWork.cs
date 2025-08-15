namespace GymGo.Application.Contracts.Persistence
{
    public interface IMembershipTypeUnitOfWork
    {
        IMembershipTypeRepositoryAsync MembershipTypeRepositoryAsync { get; }
        IMembershipTypeDapperQueriesRepositoryAsync MembershipTypeDapperQueriesRepositoryAsync { get; }
    }
}
