using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetCompaniesQuery(
        string username,
        int PageNumber,
        int PageSize
    ) : IRequest<IEnumerable<CompanyDTO>>;
}
