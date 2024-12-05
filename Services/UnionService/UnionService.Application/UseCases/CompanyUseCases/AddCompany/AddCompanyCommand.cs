using MediatR;
using Microsoft.AspNetCore.Http;

namespace UnionService.Application.UseCases
{
    public record AddCompanyCommand(
        int? userId,
        string? name,
        string? description,
        IFormFile? imageFile
    ) : IRequest<Unit>;
}