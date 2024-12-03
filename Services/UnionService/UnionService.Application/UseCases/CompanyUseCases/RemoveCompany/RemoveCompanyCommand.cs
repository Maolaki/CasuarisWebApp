using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveCompanyCommand(
        string username,
        int CompanyId
        ) : IRequest<Unit>;
}
