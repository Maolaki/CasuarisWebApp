using MediatR;
using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases
{
    public record AuthenticateUserQuery(
        string? Login,
        string? Password
    ) : IRequest<AuthenticatedDTO>;
}
