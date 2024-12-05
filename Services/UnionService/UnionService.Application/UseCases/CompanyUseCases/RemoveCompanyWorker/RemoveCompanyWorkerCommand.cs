using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record RemoveCompanyWorkerCommand(
        string? username, 
        int? companyId, 
        int? userId, 
        CompanyRole? role
        ) : IRequest<Unit>;
}
