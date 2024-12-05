using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddCompanyDateTimeCheckerCommand(
        string? username,
        int? companyId,
        DateTimeCheckerType? type,
        string? address,
        string? model
    ) : IRequest<Unit>;
}
