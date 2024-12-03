using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveInvitationCommand(
        string username,
        int CompanyId,
        int InvitationId
        ) : IRequest<Unit>;
}
