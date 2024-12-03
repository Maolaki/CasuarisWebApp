using MediatR;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public record AddResourceCommand(
        string username,
        int CompanyId,
        int TaskInfoId,
        string? ResourceData,
        byte[]? ResourceDataBytes,
        string? ContentType,
        ResourceType Type
    ) : IRequest<Unit>;
}
