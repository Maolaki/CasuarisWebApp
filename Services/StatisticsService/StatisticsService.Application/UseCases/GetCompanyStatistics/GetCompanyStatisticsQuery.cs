using MediatR;

namespace StatisticsService.Application.UseCases
{
    public record GetCompanyStatisticsQuery(
        string username,
        int CompanyId,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<Unit>;
}