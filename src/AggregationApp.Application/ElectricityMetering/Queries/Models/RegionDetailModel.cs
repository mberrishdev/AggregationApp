namespace AggregationApp.Application.ElectricityMetering.Queries.Models
{
    public class RegionDetailModel
    {
        public int RegionId { get; set; }
        public string ObjName { get; set; }
        public string ObjGvType { get; set; }
        public string ObjNumber { get; set; }
        public decimal? ConsumedElectricityPerHour { get; set; }
        public decimal? ProducedElectricityPerHour { get; set; }
        public DateTime DateTime { get; private set; }
    }
}
