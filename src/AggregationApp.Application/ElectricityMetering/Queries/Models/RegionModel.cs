namespace AggregationApp.Application.ElectricityMetering.Queries.Models
{
    public class RegionModel
    {
        public string Name { get; set; }
        public List<RegionDetailModel> RegionDetails { get; set; }
    }
}
