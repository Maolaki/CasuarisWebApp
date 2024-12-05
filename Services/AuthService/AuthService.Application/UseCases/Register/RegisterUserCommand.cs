using MediatR;

namespace AuthService.Application.UseCases
{
    public record RegisterUserCommand(
        string? username,
        string? email,
        string? password
    ) : IRequest<Unit>;
}
