using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveInvitationCommand(
        string? username,
        int? invitationId,
        bool? answer
        ) : IRequest<Unit>;
}
