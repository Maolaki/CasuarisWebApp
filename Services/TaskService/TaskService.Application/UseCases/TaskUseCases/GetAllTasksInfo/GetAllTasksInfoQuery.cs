using MediatR;
using TaskService.Application.DTOs;

namespace TaskService.Application.UseCases
{
    public record GetAllTasksInfoQuery(
        string username,
        int CompanyId
    ) : IRequest<IEnumerable<TaskInfoDTO>>;
}
