using MediatR;

namespace TaskService.Application.UseCases
{
    public record UpdateTaskCommand(
        string? username,
        int? companyId,
        int? taskId,
        string? name,
        string? description,
        decimal? budget,
        Domain.Enums.TaskStatus? status
    ) : IRequest<Unit>;
}
