using MediatR;

namespace UnionService.Application.UseCases
{
    public record UpdateTeamCommand(
        string? username,
        int? companyId,
        int? teamId,
        string? name,
        string? description
        ) : IRequest<Unit>;
}
