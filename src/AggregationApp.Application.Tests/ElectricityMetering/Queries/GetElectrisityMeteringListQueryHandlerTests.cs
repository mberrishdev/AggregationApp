using AggregationApp.Application.Contracts.Persistence.Repository;
using AggregationApp.Application.ElectricityMetering.Queries;
using AggregationApp.Application.ElectricityMetering.Queries.Models;
using AggregationApp.Domain.RegionMetering;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace AggregationApp.Application.Tests.ElectricityMetering.Queries
{
    public class GetElectrisityMeteringListQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IQueryRepository<Region>> _repository;

        private readonly GetElectrisityMeteringListQueryHandler _handler;

        public GetElectrisityMeteringListQueryHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IQueryRepository<Region>>();
            _handler = new GetElectrisityMeteringListQueryHandler(_mapper.Object, _repository.Object);
        }

        [Fact]
        public async Task GetElectrisityMeteringListQueryHandler_WhenDataExistByRegion_ShoudReturnRegionModelList()
        {
            // arrange
            var createCommand = ElectricityMeteringObjects.GetValidCreateCommand();
            var regionList = new List<Region>()
            {
                new Region(createCommand)
            };

            var regionDataModel = new List<RegionModel>() { ElectricityMeteringObjects.GetValidRegionModel() };

            var query = ElectricityMeteringObjects.GetValidGetListQuery();

            _repository.Setup(x => x.GetListAsync(It.IsAny<List<Func<IQueryable<Region>, IIncludableQueryable<Region, object>>>>(),
                It.IsAny<Expression<Func<Region, bool>>>(), null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(regionList);

            _mapper.Setup(x => x.Map<List<RegionModel>>(It.IsAny<List<Region>>()))
                .Returns(regionDataModel);

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // asset
            result.Should().BeOfType<List<RegionModel>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetElectrisityMeteringListQueryHandler_WhenDataDoesNotExistByRegion_ShoudReturnEmptyList()
        {
            //arrange
            var regionList = new List<Region>();
            var query = ElectricityMeteringObjects.GetValidGetListQuery();

            _repository.Setup(x => x.GetListAsync(It.IsAny<List<Func<IQueryable<Region>, IIncludableQueryable<Region, object>>>>(),
                It.IsAny<Expression<Func<Region, bool>>>(), null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(regionList);
            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // asset
            result.Should().BeOfType<List<RegionModel>>();
            result.Count.Should().Be(0);
        }
    }
}
