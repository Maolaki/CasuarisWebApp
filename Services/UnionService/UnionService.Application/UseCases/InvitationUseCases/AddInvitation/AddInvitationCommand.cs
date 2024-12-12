using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddInvitationCommand(
        string? username,
        int? companyId,
        string? memberUsername,
        string? description,
        CompanyRole? role,
        int? teamId,
        InvitationType? type
    ) : IRequest<Unit>;
}
