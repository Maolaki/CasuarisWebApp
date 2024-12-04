using MediatR;
using AuthService.Application.UseCases;
using AuthService.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("revoke")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            await _mediator.Send(new RevokeTokenCommand(refreshToken, User));
            return Ok();
        }

        [HttpPost("revoke-all")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> RevokeAllTokens()
        {
            await _mediator.Send(new RevokeAllTokensCommand(User));
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokensCommand command)
        {
            var newAccessToken = await _mediator.Send(command);
            return Ok(new { AccessToken = newAccessToken });
        }
    }
}