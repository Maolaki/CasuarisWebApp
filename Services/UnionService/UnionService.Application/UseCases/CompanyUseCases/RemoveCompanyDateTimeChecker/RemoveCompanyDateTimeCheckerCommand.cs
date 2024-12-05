using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveCompanyDateTimeCheckerCommand(
        string? username,
        int? companyId,
        int? dateTimeCheckerId
        ) : IRequest<Unit>;
}
