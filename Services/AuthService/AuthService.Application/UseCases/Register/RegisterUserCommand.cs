using MediatR;

namespace AuthService.Application.UseCases
{
    public record RegisterUserCommand(
        string? Username,
        string? Email,
        string? Password
    ) : IRequest<Unit>;
}
