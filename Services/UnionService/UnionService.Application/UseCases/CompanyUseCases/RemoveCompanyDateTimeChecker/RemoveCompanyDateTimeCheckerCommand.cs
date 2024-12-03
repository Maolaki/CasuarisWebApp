using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveCompanyDateTimeCheckerCommand(
        string username,
        int CompanyId,
        int DateTimeCheckerId
        ) : IRequest<Unit>;
}
