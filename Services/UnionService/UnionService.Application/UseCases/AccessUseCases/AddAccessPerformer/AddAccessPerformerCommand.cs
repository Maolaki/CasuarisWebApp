using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddAccessPerformerCommand(
        string? username,
        int? companyId,
        int? accessId,
        int? userId
        ) : IRequest<Unit>;
}
