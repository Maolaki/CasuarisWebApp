using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveAccessPerformerCommand(
        string? username,
        int? companyId,
        int? accessId,
        int? userId
        ) : IRequest<Unit>;
}
