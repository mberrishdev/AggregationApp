using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService;
using AggregationApp.Application.Contracts.Persistence.Repository;
using AggregationApp.Application.Contracts.Persistence.UnitOfWork;
using AggregationApp.Domain.RegionMetering;
using AggregationApp.Domain.RegionMetering.Commands;
using AutoMapper;
using MediatR;

namespace AggregationApp.Application.ElectricityMetering.Commands
{
    public class CreateRegionMeteringCommandHandler : IRequestHandler<CreateRegionMeteringCommand>
    {
        private readonly IElectricityMeteringService _electricityMeteringService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Region> _repository;
        public CreateRegionMeteringCommandHandler(IElectricityMeteringService electricityMeteringService,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<Region> repository)
        {
            _electricityMeteringService = electricityMeteringService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateRegionMeteringCommand request, CancellationToken cancellationToken)
        {
            var regionMeteringList = await _electricityMeteringService.GetData(cancellationToken);

            using (var scope = await _unitOfWork.CreateScopeAsync(cancellationToken))
            {
                foreach (var regionMetering in regionMeteringList)
                {
                    var regionInDb = await _repository.GetAsync(predicate: x => x.Name.Equals(regionMetering.TINKLAS),
                        cancellationToken: cancellationToken);
                    if (regionInDb is not null)
                        //this region already exist in DB
                        continue;

                    var createRegionCommand = new CreateRegionMeteringCommand()
                    {
                        RegionName = regionMetering.TINKLAS,
                        CreateRegionMeteringDetailCommands = regionMetering.RegionMeteringDetailModels
                        .Select(x => new CreateRegionMeteringDetailCommand()
                        {
                            ObjName = x.OBT_PAVADINIMAS,
                            ObjGvType = x.OBJ_GV_TIPAS,
                            ObjNumber = x.OBJ_NUMERIS,
                            ProducedElectricityPerHour = decimal.TryParse(x.Pp, out decimal peph) ? peph : 0,
                            DateTime = DateTime.TryParse(x.PL_T, out DateTime dt) ? dt : DateTime.Now,
                            ConsumedElectricityPerHour = decimal.TryParse(x.Pm, out decimal ceph) ? ceph : 0,
                        }).ToList(),
                    };

                    var region = new Region(createRegionCommand);
                    await _repository.InsertAsync(region, cancellationToken);
                }

                await scope.CompletAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
