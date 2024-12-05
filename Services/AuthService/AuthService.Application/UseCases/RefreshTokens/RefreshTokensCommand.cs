using MediatR;

namespace AuthService.Application.UseCases
{
    public record RefreshTokensCommand(
        string? accessToken,
        string? refreshToken
    ) : IRequest<string>;
}
