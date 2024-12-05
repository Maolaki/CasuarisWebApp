using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddTeamCommand(
        string? username,
        int? companyId,
        string? name,
        string? description
        ) : IRequest<Unit>;
}
