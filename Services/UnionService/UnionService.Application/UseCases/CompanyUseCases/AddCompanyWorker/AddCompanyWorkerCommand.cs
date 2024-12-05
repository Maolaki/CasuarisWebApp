using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddCompanyWorkerCommand(
        string? username,
        int? userId, 
        int? companyId, 
        CompanyRole? role,
        decimal? salary,
        int? workHours, 
        int? workDays
        ) : IRequest<Unit>;
}
