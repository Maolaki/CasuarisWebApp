using MediatR;

namespace TaskService.Application.UseCases
{
    public record ChangeResourcePositionCommand(
        string? username,
        int? companyId,
        int? taskInfoId,
        int? resourceId,
        int? newPosition
    ) : IRequest<Unit>;
}
