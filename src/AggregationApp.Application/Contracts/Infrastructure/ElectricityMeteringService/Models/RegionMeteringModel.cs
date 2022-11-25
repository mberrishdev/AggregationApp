namespace AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService.Models
{
    public class RegionMeteringModel
    {
        public string TINKLAS { get; set; }
        public List<RegionMeteringDetailModel> RegionMeteringDetailModels { get; set; }
    }
}
