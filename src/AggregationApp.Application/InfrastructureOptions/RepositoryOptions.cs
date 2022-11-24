using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Application.InfrastrucgtreOptions
{
    public class RepositoryOptions<TDbContext> where TDbContext : DbContext
    {
        public SaveChangeStrategy SaveChangeStrategy { get; set; } = SaveChangeStrategy.PerUnitOfWork;
    }

    public enum SaveChangeStrategy
    {
        PerOperation,
        PerUnitOfWork
    }
}
