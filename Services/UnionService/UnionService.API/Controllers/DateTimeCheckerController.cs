using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using UnionService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using UnionService.API.Filters;

namespace UnionService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DateTimeCheckerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DateTimeCheckerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWorkLog([FromForm] AddWorkLogCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
