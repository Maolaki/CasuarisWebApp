using MediatR;
using Microsoft.AspNetCore.Http;

namespace UnionService.Application.UseCases
{
    public record UpdateCompanyCommand(
        string username,
        int CompanyId,
        string? Name,
        string? Description,
        IFormFile? ImageFile
        ) : IRequest<Unit>;
}
