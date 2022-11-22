using AggregationApp.Application.Models;

namespace AggregationApp.Application.Infrastructure.Contracts
{
    public interface IMeteringPlantApiService
    {
        Task<List<MeteringPlantModel>> LoadCSVData(string year, string month);
    }
}
