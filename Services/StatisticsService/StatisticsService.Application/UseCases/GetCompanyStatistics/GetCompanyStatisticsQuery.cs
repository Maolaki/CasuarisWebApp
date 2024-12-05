using MediatR;

namespace StatisticsService.Application.UseCases
{
    public record GetCompanyStatisticsQuery(
        string? username,
        int? companyId,
        DateTime? startDate,
        DateTime? endDate
    ) : IRequest<Unit>;
}