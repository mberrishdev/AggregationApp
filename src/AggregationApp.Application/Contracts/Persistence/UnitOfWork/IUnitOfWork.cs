namespace AggregationApp.Application.Contracts.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<IUnitOfWorkScope> CreateScopeAsync(CancellationToken cancellationToken = default);
    }
}
