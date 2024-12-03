using MediatR;

namespace TaskService.Application.UseCases
{
    public record RemoveTaskCommand(
        string username,
        int CompanyId,
        int TaskId
        ) : IRequest<Unit>;
}
