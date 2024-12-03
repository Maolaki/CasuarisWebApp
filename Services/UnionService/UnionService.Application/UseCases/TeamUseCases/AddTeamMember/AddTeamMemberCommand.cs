using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddTeamMemberCommand(
        string username,
        int CompanyId,
        int TeamId,
        int UserId
        ) : IRequest<Unit>;
}
