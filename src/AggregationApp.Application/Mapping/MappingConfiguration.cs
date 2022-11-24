using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService.Models;
using AggregationApp.Application.ElectricityMetering.Queries.Models;
using AggregationApp.Domain.RegionMetering;
using AggregationApp.Domain.RegionMetering.Commands;
using AutoMapper;

namespace AggregationApp.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionMeteringModel, CreateRegionMeteringCommand>()
                .ForMember(x => x.RegionName, y => y.MapFrom(c => c.TINKLAS)).ReverseMap()
                .ForMember(x => x.RegionMeteringDetailModel, y => y.MapFrom(c => c.CreateRegionMeteringDetailCommands)).ReverseMap();

            CreateMap<RegionMeteringDetailModel, CreateRegionMeteringDetailCommand>()
                .ForMember(x => x.ObjName, y => y.MapFrom(c => c.OBT_PAVADINIMAS))
                .ForMember(x => x.ObjGvType, y => y.MapFrom(c => c.OBJ_GV_TIPAS))
                .ForMember(x => x.ObjNumber, y => y.MapFrom(c => c.OBJ_NUMERIS))
                .ForMember(x => x.ProducedElectricityPerHour, y => y.MapFrom(c => c.Pp))
                .ForMember(x => x.DateTime, y => y.MapFrom(c => c.PL_T))
                .ForMember(x => x.ConsumedElectricityPerHour, y => y.MapFrom(c => c.Pm)).ReverseMap();

            CreateMap<Region, RegionModel>().ReverseMap();
            CreateMap<RegionDetail, RegionDetailModel>().ReverseMap();

        }
    }
}
