using MediatR;

namespace TaskService.Application.UseCases
{
    public record AddTaskCommand(
        string? username,
        int? companyId,
        int? parentId,
        string? name,
        string? description,
        decimal? budget
    ) : IRequest<Unit>;
}
