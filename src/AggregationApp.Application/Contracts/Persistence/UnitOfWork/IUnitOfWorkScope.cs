namespace AggregationApp.Application.Contracts.Persistence.UnitOfWork
{
    public interface IUnitOfWorkScope : IDisposable
    {
        Task CompletAsync(CancellationToken cancellationToken = default);
    }
}
