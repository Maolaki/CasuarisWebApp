using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddTeamCommand(
        string Name,
        string Description,
        string username,
        int CompanyId
        ) : IRequest<Unit>;
}
