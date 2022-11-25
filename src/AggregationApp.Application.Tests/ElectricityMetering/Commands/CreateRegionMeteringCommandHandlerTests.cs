using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService;
using AggregationApp.Application.Contracts.Persistence.Repository;
using AggregationApp.Application.Contracts.Persistence.UnitOfWork;
using AggregationApp.Application.ElectricityMetering.Commands;
using AggregationApp.Domain.RegionMetering;
using AggregationApp.Domain.RegionMetering.Commands;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace AggregationApp.Application.Tests.ElectricityMetering.Commands
{
    public class CreateRegionMeteringCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IElectricityMeteringService> _electricityMeteringService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRepository<Region>> _repository;

        private readonly CreateRegionMeteringCommandHandler _handler;

        public CreateRegionMeteringCommandHandlerTests()
        {
            _electricityMeteringService = new Mock<IElectricityMeteringService>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IRepository<Region>>();
            _handler = new CreateRegionMeteringCommandHandler(_electricityMeteringService.Object,
                _mapper.Object, _unitOfWork.Object, _repository.Object);
        }

        [Fact]
        public async Task CreateRegionMeteringCommandHandler_WhenRegionMeteringListNotEmpty_ShouldCreateNewDataInDb()
        {
            // arrange
            var regionMeteringList = ElectricityMeteringObjects.GetRegionMeteringList();
            var command = ElectricityMeteringObjects.GetValidCreateCommand();
            var region = new Region(command);

            _electricityMeteringService.Setup(x => x.GetData(It.IsAny<CancellationToken>()))
                .ReturnsAsync(regionMeteringList);

            _unitOfWork.Setup(x => x.CreateScopeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Mock<IUnitOfWorkScope>().Object);

            _repository.SetupSequence(x => x.GetAsync(It.IsAny<Expression<Func<Region, bool>>>(),
                It.IsAny<List<Func<IQueryable<Region>, IIncludableQueryable<Region, object>>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(region)
                .ReturnsAsync((Region)null);

            // act
            Func<Task> action = async () => await _handler.Handle(new CreateRegionMeteringCommand(), It.IsAny<CancellationToken>());

            // assert
            using var scope = new AssertionScope();
            await action.Should().NotThrowAsync();
            _repository.Verify(x => x.InsertAsync(It.IsAny<Region>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
