using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService.Models;

namespace AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService
{
    public interface IElectricityMeteringService
    {
        Task<List<RegionMeteringModel>> GetData(CancellationToken cancellationToken);
    }
}
