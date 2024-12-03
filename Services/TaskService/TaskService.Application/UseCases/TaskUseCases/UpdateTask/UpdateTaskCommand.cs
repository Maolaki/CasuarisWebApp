using MediatR;

namespace TaskService.Application.UseCases
{
    public record UpdateTaskCommand(
        string username,
        int CompanyId,
        int TaskId,
        string? Name,
        string? Description,
        decimal? Budget,
        Domain.Enums.TaskStatus? Status
    ) : IRequest<Unit>;
}
