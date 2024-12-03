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
    public class AccessController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccessController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("add-performer")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> AddAccessPerformer([FromBody] AddAccessPerformerCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("remove-performer")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> RemoveAccessPerformer([FromBody] RemoveAccessPerformerCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }
    }
}
