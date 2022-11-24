using AggregationApp.Domain.Common;
using AggregationApp.Domain.RegionMetering.Commands;
using System.ComponentModel.DataAnnotations;

namespace AggregationApp.Domain.RegionMetering
{
    public class Region : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; private set; }
        public List<RegionDetail> RegionDetails { get; private set; }

        private Region() { }

        public Region(CreateRegionMeteringCommand command)
        {
            Name = command.RegionName;
            RegionDetails = command.CreateRegionMeteringDetailCommands?
                .Select(regionDetailCommand => new RegionDetail(regionDetailCommand)).ToList();
        }
    }
}
