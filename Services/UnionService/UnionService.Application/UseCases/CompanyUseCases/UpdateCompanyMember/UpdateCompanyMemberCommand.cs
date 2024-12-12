using MediatR;

namespace UnionService.Application.UseCases
{
    public record UpdateCompanyMemberCommand(
        string? username,
        int? companyId,
        int? memberId,
        decimal? salary,
        int? workHours,
        int? workDays
        ) : IRequest<Unit>;
}
