using MediatR;

namespace UnionService.Application.UseCases
{
    public record RemoveCompanyWorkerCommand(
        string username, 
        int CompanyId, 
        int UserId, 
        string Role
        ) : IRequest<Unit>;
}
