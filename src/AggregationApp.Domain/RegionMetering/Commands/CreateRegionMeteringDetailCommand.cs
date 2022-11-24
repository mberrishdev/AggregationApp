using MediatR;

namespace AggregationApp.Domain.RegionMetering.Commands
{
    public class CreateRegionMeteringDetailCommand : IRequest
    {
        public string ObjName { get; set; }
        public string ObjGvType { get; set; }
        public string ObjNumber { get; set; }
        public decimal? ConsumedElectricityPerHour { get; set; }
        public decimal? ProducedElectricityPerHour { get; set; }
        public DateTime DateTime { get; set; }
    }
}
