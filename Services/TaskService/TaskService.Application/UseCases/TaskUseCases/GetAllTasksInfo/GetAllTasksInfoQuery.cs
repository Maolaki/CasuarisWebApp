using MediatR;
using TaskService.Application.DTOs;

namespace TaskService.Application.UseCases
{
    public record GetAllTasksInfoQuery(
        string? username,
        int? companyId
    ) : IRequest<IEnumerable<TaskInfoDTO>>;
}
