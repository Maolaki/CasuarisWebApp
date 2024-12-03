using MediatR;
using UnionService.Domain.Interfaces;
using UnionService.Domain.Entities;

namespace UnionService.Application.UseCases
{
    public class AddTeamMemberHandler : IRequestHandler<AddTeamMemberCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddTeamMemberHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
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

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.UserId} does not exist.");
            }

            if (existingTeam.Members?.Any(m => m.Id == existingUser.Id) == true)
            {
                throw new ArgumentException($"User with Id {request.UserId} is already a member of the team.");
            }

            if (existingTeam.Members == null)
            {
                existingTeam.Members = new List<User>();
            }

            existingTeam.Members.Add(existingUser);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
