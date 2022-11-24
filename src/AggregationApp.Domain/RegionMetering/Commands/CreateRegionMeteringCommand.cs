using MediatR;

namespace AggregationApp.Domain.RegionMetering.Commands
{
    public class CreateRegionMeteringCommand : IRequest
    {
        public string RegionName { get; set; }
        public List<CreateRegionMeteringDetailCommand> CreateRegionMeteringDetailCommands { get; set; }
    }
}
