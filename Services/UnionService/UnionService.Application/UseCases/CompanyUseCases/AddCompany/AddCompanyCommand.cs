using MediatR;
using Microsoft.AspNetCore.Http;

namespace UnionService.Application.UseCases
{
    public record AddCompanyCommand(
        int UserId,
        string? Name,
        string? Description,
        IFormFile? ImageFile
    ) : IRequest<Unit>;
}