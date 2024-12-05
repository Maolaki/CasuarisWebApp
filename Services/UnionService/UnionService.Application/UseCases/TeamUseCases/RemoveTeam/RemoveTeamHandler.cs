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
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingTeam = existingCompany.Teams!.FirstOrDefault(t => t.Id == request.teamId);
            if (existingTeam == null)
            {
                throw new ArgumentException($"Team with Id {request.teamId} does not exist.");
            }

            var invitationsToRemove = await _unitOfWork.Invitations.GetAllAsync(i => i.TeamId == existingTeam.Id, 1, int.MaxValue);

            if (invitationsToRemove != null)
            {
                foreach (var invitation in invitationsToRemove)
                    _unitOfWork.Invitations.Delete(invitation);
            }

            _unitOfWork.Teams.Delete(existingTeam);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
