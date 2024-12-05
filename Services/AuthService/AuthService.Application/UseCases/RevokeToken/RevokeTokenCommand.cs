using MediatR;
using System.Security.Claims;

namespace AuthService.Application.UseCases
{
    public record RevokeTokenCommand(
        string? refreshToken, 
        ClaimsPrincipal? user
        ) : IRequest<Unit>;
}
