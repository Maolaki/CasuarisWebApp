using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record GetCompanyRoleQuery(
        string? username,
        int? companyId
    ) : IRequest<CompanyRole>;
}
