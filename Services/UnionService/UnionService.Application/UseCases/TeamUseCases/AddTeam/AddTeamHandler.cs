using MediatR;
using UnionService.Domain.Interfaces;
using UnionService.Domain.Entities;

namespace UnionService.Application.UseCases
{
    public class AddTeamHandler : IRequestHandler<AddTeamCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddTeamHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var team = new Team
            {
                Name = request.Name,
                Description = request.Description,
                CompanyId = request.CompanyId
            };

            _unitOfWork.Teams.Create(team);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
