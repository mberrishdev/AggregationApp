using AggregationApp.Application.ElectricityMetering.Queries;
using AggregationApp.Application.ElectricityMetering.Queries.Models;
using AggregationApp.Domain.RegionMetering.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AggregationApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ElectricityMeteringController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ElectricityMeteringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetElectricityMeteringData/{regionName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<RegionModel>>> GetElectricityMeteringData(
            [Required, FromRoute] string regionName,
            int pageIndex, int pageSize)
        {
            var result = await _mediator.Send(new GetElectrisityMeteringListQuery()
            {
                RegionName = regionName,
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            return Ok(result);
        }

        [HttpPost("CreateElectricityMeteringData")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateElectricityMeteringData()
        {
            await _mediator.Send(new CreateRegionMeteringCommand());
            return Ok();
        }
    }
}
