using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddInvitationCommand(
        string? Description,
        int UserId,
        string username,
        int CompanyId,
        int? TeamId,
        InvitationType Type
    ) : IRequest<Unit>;
}
