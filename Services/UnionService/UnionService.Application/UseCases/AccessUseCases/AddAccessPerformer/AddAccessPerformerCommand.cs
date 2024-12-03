using MediatR;

namespace UnionService.Application.UseCases
{
    public record AddAccessPerformerCommand(
        string username,
        int CompanyId,
        int AccessId,
        int UserId
        ) : IRequest<Unit>;
}
