using MediatR;
using System.Security.Claims;

namespace AuthService.Application.UseCases
{
    public record RevokeAllTokensCommand(ClaimsPrincipal? user) : IRequest<Unit>;
}
