using MediatR;
using System.Security.Claims;

namespace AuthService.Application.UseCases
{
    public record GetUserIdQuery(ClaimsPrincipal ClaimsPrincipalIdentity) : IRequest<long>;
}
