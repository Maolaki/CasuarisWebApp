using MediatR;
using System.Security.Claims;

namespace AuthService.Application.UseCases
{
    public record RevokeTokenCommand(string RefreshToken, ClaimsPrincipal User) : IRequest<Unit>;
}
