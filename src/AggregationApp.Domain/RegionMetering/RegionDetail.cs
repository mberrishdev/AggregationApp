using AggregationApp.Domain.Common;
using AggregationApp.Domain.RegionMetering.Commands;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AggregationApp.Domain.RegionMetering
{
    public class RegionDetail : BaseEntity
    {
        [Required]
        public int RegionId { get; private set; }
        [Required, MaxLength(200)]
        public string ObjName { get; private set; }
        [Required, MaxLength(50)]
        public string ObjGvType { get; private set; }
        [Required, MaxLength(200)]
        public string ObjNumber { get; private set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ConsumedElectricityPerHour { get; private set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ProducedElectricityPerHour { get; private set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateTime { get; private set; }

        private RegionDetail() { }
        public RegionDetail(CreateRegionMeteringDetailCommand command)
        {
            ObjName = command.ObjName;
            ObjGvType = command.ObjGvType;
            ObjNumber = command.ObjNumber;
            ConsumedElectricityPerHour = command.ConsumedElectricityPerHour;
            ProducedElectricityPerHour = command.ProducedElectricityPerHour;
            DateTime = command.DateTime;
        }
    }
}
