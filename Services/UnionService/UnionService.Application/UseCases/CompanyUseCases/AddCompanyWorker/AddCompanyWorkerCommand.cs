using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddCompanyWorkerCommand(
        string username,
        int UserId, 
        int CompanyId, 
        string Role,
        decimal? Salary,
        int? WorkHours, 
        int? WorkDays) : IRequest<Unit>;
}
