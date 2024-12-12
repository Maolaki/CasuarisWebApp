using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Enums;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddInvitationHandler : IRequestHandler<AddInvitationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddInvitationHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddInvitationCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Username == request.memberUsername);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with username {request.memberUsername} does not exist.");
            }

            if (existingCompany.Owners!.Contains(existingUser) 
                || existingCompany.Managers!.Contains(existingUser) 
                || existingCompany.Performers!.FirstOrDefault(p => p.User == existingUser) != null)
            {
                throw new ArgumentException($"User with id {request.memberUsername} already in company.");
            }

            if (request.type == Domain.Enums.InvitationType.team)
            {
                var existingTeam = existingCompany.Teams!.FirstOrDefault(t => t.Id == request.teamId);
                if (existingTeam == null)
                {
                    throw new ArgumentException($"Team with Id {request.teamId} does not exist.");
                }
            }

            var invitation = new Invitation
            {
                Description = request.description,
                UserId = existingUser.Id,
                CompanyId = (int)request.companyId!,
                Role = request.role,
                TeamId = request.teamId,
                Type = (InvitationType)request.type!,
            };

            _unitOfWork.Invitations.Create(invitation);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
