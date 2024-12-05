using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetCompaniesQuery(
        string? username,
        int? pageNumber,
        int? pageSize
    ) : IRequest<IEnumerable<CompanyDTO>>;
}
