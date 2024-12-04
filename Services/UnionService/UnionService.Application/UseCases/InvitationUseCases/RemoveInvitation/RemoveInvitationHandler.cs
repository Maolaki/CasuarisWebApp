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
            var existingInvitation = await _unitOfWork.Invitations.GetAsync(i => i.Id == request.InvitationId && i.CompanyId == request.CompanyId);
            if (existingInvitation == null)
            {
                throw new ArgumentException($"Invitation with Id {request.InvitationId} does not exist.");
            }

            if (existingInvitation.User!.Username != request.username)
                throw new ArgumentException("User have no permisson");

            _unitOfWork.Invitations.Delete(existingInvitation);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
