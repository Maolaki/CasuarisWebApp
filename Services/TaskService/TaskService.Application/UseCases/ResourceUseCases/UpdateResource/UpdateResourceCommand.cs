using MediatR;
using Microsoft.AspNetCore.Http;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public record UpdateResourceCommand(
        string? username,
        int? companyId,
        int? resourceId,
        string? data,
        IFormFile? imageFile,
        ResourceType? type
    ) : IRequest<Unit>;
}
