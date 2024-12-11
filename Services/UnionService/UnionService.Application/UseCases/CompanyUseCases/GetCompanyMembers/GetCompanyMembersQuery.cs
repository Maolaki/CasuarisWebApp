using MediatR;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public record GetCompanyMembersQuery(
        string? username,
        int? companyId
    ) : IRequest<IEnumerable<CompanyMemberDTO>>;
}
