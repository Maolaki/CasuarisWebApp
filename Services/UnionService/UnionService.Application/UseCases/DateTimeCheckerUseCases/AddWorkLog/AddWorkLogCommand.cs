using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddWorkLogCommand(
        string? username,
        int? companyId,
        DateTime? workDate,
        TimeSpan? hoursWorked
    ) : IRequest<Unit>;
}
