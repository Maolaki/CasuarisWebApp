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
    public class InvitationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InvitationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("add")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> AddInvitation([FromBody] AddInvitationCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("remove")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> RemoveInvitation([FromBody] RemoveInvitationCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }
    }
}
