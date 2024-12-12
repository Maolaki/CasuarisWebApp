using MediatR;

namespace AuthService.Application.UseCases
{
    public record UpdateUserCommand(
        string? username,
        string? newUsername,
        string? newEmail
        ) : IRequest<Unit>;
}
