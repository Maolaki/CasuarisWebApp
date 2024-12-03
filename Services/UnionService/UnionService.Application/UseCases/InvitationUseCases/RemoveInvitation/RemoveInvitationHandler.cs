using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveInvitationHandler : IRequestHandler<RemoveInvitationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveInvitationHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveInvitationCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingInvitation = await _unitOfWork.Invitations.GetAsync(i => i.Id == request.InvitationId && i.CompanyId == request.CompanyId);
            if (existingInvitation == null)
            {
                throw new ArgumentException($"Invitation with Id {request.InvitationId} does not exist.");
            }

            _unitOfWork.Invitations.Delete(existingInvitation);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
