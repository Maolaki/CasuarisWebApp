using MediatR;

namespace TaskService.Application.UseCases
{
    public record RemoveResourceCommand(
        string username,
        int CompanyId,
        int ResourceId) : IRequest<Unit>;
}
