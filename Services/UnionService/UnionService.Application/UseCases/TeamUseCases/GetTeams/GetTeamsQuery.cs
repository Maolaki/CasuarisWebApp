using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetTeamsQuery(
        string username,
        int CompanyId,
        int PageNumber,
        int PageSize
    ) : IRequest<IEnumerable<TeamDTO>>;
}
