using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddCompanyCommand(
        int UserId,
        string? Name,
        string? Description,
        string? LogoContentType,
        byte[]? LogoData
    ) : IRequest<Unit>;
}