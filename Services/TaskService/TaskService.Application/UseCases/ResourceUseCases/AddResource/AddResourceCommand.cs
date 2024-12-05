using MediatR;
using Microsoft.AspNetCore.Http;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public record AddResourceCommand(
        string? username,
        int? companyId,
        int? taskInfoId,
        string? resourceData,
        IFormFile? imageFile,
        ResourceType? type
    ) : IRequest<Unit>;
}
