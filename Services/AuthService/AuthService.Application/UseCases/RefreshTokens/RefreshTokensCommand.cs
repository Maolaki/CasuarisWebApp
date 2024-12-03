using MediatR;

namespace AuthService.Application.UseCases
{
    public record RefreshTokensCommand(
        string? AccessToken,
        string? RefreshToken
    ) : IRequest<string>;
}
