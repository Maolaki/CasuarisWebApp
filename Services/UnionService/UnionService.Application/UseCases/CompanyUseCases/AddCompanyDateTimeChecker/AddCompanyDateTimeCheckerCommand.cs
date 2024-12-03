using MediatR;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public record AddCompanyDateTimeCheckerCommand(
        string username,
        int CompanyId,
        DateTimeCheckerType Type,
        string? Address,
        string? Model
    ) : IRequest<Unit>;
}
