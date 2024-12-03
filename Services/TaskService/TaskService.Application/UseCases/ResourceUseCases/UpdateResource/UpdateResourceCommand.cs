using MediatR;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public record UpdateResourceCommand(
        string username,
        int CompanyId,
        int ResourceId,
        string? Data,
        byte[]? DataBytes,
        string? ContentType,
        ResourceType Type
    ) : IRequest<Unit>;
}
