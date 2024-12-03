using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveTeamHandler : IRequestHandler<RemoveTeamCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveTeamHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingTeam = existingCompany.Teams!.FirstOrDefault(t => t.Id == request.TeamId);
            if (existingTeam == null)
            {
                throw new ArgumentException($"Team with Id {request.TeamId} does not exist.");
            }

            _unitOfWork.Teams.Delete(existingTeam);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
