using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetCompanyQuery(
        string? username,
        int? companyId
    ) : IRequest<CompanyDTO>;
}
