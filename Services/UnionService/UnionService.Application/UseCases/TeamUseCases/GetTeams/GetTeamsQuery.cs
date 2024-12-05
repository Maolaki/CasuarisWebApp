using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetTeamsQuery(
        string? username,
        int? companyId,
        int? pageNumber,
        int? pageSize
    ) : IRequest<IEnumerable<TeamDTO>>;
}
