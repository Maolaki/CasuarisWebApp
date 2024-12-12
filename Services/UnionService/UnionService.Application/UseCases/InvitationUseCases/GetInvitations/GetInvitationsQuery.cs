using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetInvitationsQuery(
        string? username,
        int? pageNumber,
        int? pageSize
    ) : IRequest<IEnumerable<InvitationDTO>>;
}
