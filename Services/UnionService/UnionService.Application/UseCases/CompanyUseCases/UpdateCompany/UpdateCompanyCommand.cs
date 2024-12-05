using MediatR;
using Microsoft.AspNetCore.Http;

namespace UnionService.Application.UseCases
{
    public record UpdateCompanyCommand(
        string? username,
        int? companyId,
        string? name,
        string? description,
        IFormFile? imageFile
        ) : IRequest<Unit>;
}
