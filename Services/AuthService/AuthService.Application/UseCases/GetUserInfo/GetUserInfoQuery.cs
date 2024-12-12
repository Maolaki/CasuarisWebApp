using AuthService.Application.DTOs;
using MediatR;

namespace AuthService.Application.UseCases
{
    public record GetUserInfoQuery(
        string? username
    ) : IRequest<UserDTO>;
}
