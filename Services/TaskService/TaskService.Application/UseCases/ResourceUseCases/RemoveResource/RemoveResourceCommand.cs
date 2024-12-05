using MediatR;

namespace TaskService.Application.UseCases
{
    public record RemoveResourceCommand(
        string? username,
        int? companyId,
        int? resourceId
        ) : IRequest<Unit>;
}
