using MediatR;

namespace UnionService.Application.UseCases
{
    public record UpdateTeamCommand(
        string username,
        int CompanyId,
        int TeamId,
        string? Name,
        string? Description
        ) : IRequest<Unit>;
}
