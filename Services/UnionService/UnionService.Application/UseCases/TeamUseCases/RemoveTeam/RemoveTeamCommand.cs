using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveTeamCommand(
        string username,
        int CompanyId,
        int TeamId
        ) : IRequest<Unit>;
}
