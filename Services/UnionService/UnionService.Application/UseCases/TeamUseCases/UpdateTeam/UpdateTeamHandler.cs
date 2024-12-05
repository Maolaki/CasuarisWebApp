using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public UpdateTeamHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
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

            existingTeam.Name = request.name ?? existingTeam.Name;
            existingTeam.Description = request.description ?? existingTeam.Description;

            _unitOfWork.Teams.Update(existingTeam);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
