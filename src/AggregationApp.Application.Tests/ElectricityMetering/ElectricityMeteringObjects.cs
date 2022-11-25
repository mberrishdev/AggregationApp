using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService.Models;
using AggregationApp.Application.ElectricityMetering.Queries;
using AggregationApp.Application.ElectricityMetering.Queries.Models;
using AggregationApp.Domain.RegionMetering.Commands;

namespace AggregationApp.Application.Tests.ElectricityMetering
{
    public static class ElectricityMeteringObjects
    {
        public static List<RegionMeteringModel> GetRegionMeteringList() => new()
        {
            new RegionMeteringModel()
            {
                TINKLAS="Tinklas",
                RegionMeteringDetailModels = new()
                {
                    new RegionMeteringDetailModel()
                    {

                    }
                }
            },
                new RegionMeteringModel()
            {
                TINKLAS="Tinklas",
                RegionMeteringDetailModels = new()
                {
                    new RegionMeteringDetailModel()
                    {

                    }
                }
            }
        };

        public static CreateRegionMeteringCommand GetValidCreateCommand() => new()
        {
            RegionName = "Region",
            CreateRegionMeteringDetailCommands = new()
            {
                new CreateRegionMeteringDetailCommand()
                {
                    ObjName = "Name"
                }
            }
        };

        public static RegionModel GetValidRegionModel() => new()
        {
            Name = "Name"
        };

        public static GetElectrisityMeteringListQuery GetValidGetListQuery() => new()
        {
            RegionName = "Name"
        };
    }
}
