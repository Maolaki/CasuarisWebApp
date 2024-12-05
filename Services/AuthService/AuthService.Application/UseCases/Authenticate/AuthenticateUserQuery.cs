using MediatR;
using AuthService.Application.DTOs;

namespace AuthService.Application.UseCases
{
    public record AuthenticateUserQuery(
        string? login,
        string? password
    ) : IRequest<AuthenticatedDTO>;
}
