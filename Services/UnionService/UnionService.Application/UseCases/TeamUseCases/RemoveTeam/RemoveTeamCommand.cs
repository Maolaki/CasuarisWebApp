using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveTeamCommand(
        string? username,
        int? companyId,
        int? teamId
        ) : IRequest<Unit>;
}
