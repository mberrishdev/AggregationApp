using AggregationApp.Application.Contracts.Persistence.Repository;
using AggregationApp.Application.ElectricityMetering.Queries.Models;
using AggregationApp.Domain.RegionMetering;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AggregationApp.Application.ElectricityMetering.Queries
{
    public class GetElectrisityMeteringListQuery : IRequest<List<RegionModel>>
    {
        public string? RegionName { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 50;
    }

    public class GetElectrisityMeteringListQueryHandler : IRequestHandler<GetElectrisityMeteringListQuery, List<RegionModel>>
    {
        private readonly IMapper _mapper;
        private readonly IQueryRepository<Region> _queryRepository;

        public GetElectrisityMeteringListQueryHandler(IMapper mapper, IQueryRepository<Region> queryRepository)
        {
            _mapper = mapper;
            _queryRepository = queryRepository;
        }

        public async Task<List<RegionModel>> Handle(GetElectrisityMeteringListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Region, bool>> predicate = x => request.RegionName.Equals(x.Name);
            var relatedProperties = new List<Func<IQueryable<Region>, IIncludableQueryable<Region, object>>>
            {
                x => x.Include(x => x.RegionDetails.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize))
            };

            var regionList = await _queryRepository.GetListAsync(predicate: predicate,
                relatedProperties: relatedProperties,
                cancellationToken: cancellationToken);

            if (regionList.Any())
                return _mapper.Map<List<RegionModel>>(regionList);

            return new List<RegionModel>();
        }
    }
}
