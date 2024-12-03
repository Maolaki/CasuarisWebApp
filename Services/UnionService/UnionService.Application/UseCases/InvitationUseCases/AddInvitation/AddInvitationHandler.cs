using MediatR;
using UnionService.Domain.Entities;
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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.UserId} does not exist.");
            }

            switch (request.Type)
            {

                case Domain.Enums.InvitationType.Team:
                    var existingTeam = existingCompany.Teams!.FirstOrDefault(t => t.Id == request.TeamId);
                    if (existingTeam == null)
                    {
                        throw new ArgumentException($"Team with Id {request.TeamId} does not exist.");
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            var invitation = new Invitation
            {
                Description = request.Description,
                UserId = request.UserId,
                CompanyId = request.CompanyId,
                TeamId = request.TeamId,
                Type = request.Type,
            };

            _unitOfWork.Invitations.Create(invitation);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
