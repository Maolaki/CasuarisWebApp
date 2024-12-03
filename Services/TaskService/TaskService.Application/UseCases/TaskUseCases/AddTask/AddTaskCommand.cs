using MediatR;

namespace TaskService.Application.UseCases
{
    public record AddTaskCommand(
        string username,
        int CompanyId,
        int? ParentId,
        string? Name,
        string? Description,
        decimal Budget
    ) : IRequest<Unit>;
}
