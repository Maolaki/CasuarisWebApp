using AuthService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AuthService.API.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AuthService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get-info")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> GetUserInfo([FromBody] GetUserInfoQuery query)
        {
            if (User.Identity!.Name != query.username)
                return BadRequest("User is not authenticated.");

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("update-user")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }
    }
}