using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StatisticsService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using StatisticsService.API.Filters;

namespace StatisticsService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReportController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("companyReport")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> GetCompanyStatistics([FromBody] GetCompanyStatisticsQuery query)
        {
            if (User.Identity!.Name != query.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(query);
            return Ok();
        }
    }
}