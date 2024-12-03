using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveAccessPerformerCommand(
        string username,
        int CompanyId,
        int AccessId,
        int UserId
        ) : IRequest<Unit>;
}
