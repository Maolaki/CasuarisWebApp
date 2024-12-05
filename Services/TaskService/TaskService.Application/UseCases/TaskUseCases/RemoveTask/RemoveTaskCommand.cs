using MediatR;

namespace TaskService.Application.UseCases
{
    public record RemoveTaskCommand(
        string? username,
        int? companyId,
        int? taskId
        ) : IRequest<Unit>;
}
