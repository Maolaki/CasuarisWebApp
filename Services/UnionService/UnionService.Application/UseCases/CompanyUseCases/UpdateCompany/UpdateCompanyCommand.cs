using MediatR;

namespace UnionService.Application.UseCases
{
    public record UpdateCompanyCommand(
        string username,
        int CompanyId,
        string? Name,
        string? Description,
        string? LogoContentType,
        byte[]? LogoData) : IRequest<Unit>;
}
