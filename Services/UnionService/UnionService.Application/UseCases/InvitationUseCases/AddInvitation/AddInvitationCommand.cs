using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddInvitationCommand(
        string? username,
        string? description,
        int? userId,
        int? companyId,
        CompanyRole? role,
        int? teamId,
        InvitationType? type
    ) : IRequest<Unit>;
}
