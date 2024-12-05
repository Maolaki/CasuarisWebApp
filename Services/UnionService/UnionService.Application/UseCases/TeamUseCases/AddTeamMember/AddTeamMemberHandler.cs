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

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.userId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.userId} does not exist.");
            }

            if (existingTeam.Members?.Any(m => m.Id == existingUser.Id) == true)
            {
                throw new ArgumentException($"User with Id {request.userId} is already a member of the team.");
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
