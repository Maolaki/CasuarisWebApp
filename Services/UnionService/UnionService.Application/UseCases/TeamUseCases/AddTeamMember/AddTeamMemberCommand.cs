using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddTeamMemberCommand(
        string? username,
        int? companyId,
        int? teamId,
        int? userId
        ) : IRequest<Unit>;
}
