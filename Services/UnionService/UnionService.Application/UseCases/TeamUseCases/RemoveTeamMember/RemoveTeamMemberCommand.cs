using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveTeamMemberCommand(
        string? username,
        int? companyId,
        int? teamId,
        int? userId
        ) : IRequest<Unit>;
}
