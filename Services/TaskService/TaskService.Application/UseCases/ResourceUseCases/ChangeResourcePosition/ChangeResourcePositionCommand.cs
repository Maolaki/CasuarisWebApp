using MediatR;

namespace TaskService.Application.UseCases
{
    public record ChangeResourcePositionCommand(
        string username,
        int CompanyId,
        int TaskInfoId,
        int ResourceId,
        int NewPosition
    ) : IRequest<Unit>;
}
