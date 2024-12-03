using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveTeamMemberCommand(
        string username,
        int CompanyId,
        int TeamId,
        int UserId
        ) : IRequest<Unit>;
}
