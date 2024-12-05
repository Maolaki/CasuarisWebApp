using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Enums;
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
            var existingInvitation = await _unitOfWork.Invitations.GetAsync(i => i.Id == request.invitationId);
            if (existingInvitation == null)
            {
                throw new ArgumentException($"Invitation with Id {request.invitationId} does not exist.");
            }

            if (existingInvitation.User!.Username != request.username)
                throw new ArgumentException("User have no permisson");

            switch (existingInvitation.Type)
            {
                case InvitationType.company:
                    if ((bool)request.answer!)
                    {
                        switch (existingInvitation.Role) 
                        {
                            case CompanyRole.owner:
                                existingInvitation.Company!.Owners!.Add(existingInvitation.User);
                                break;

                            case CompanyRole.manager:
                                existingInvitation.Company!.Managers!.Add(existingInvitation.User);
                                break;

                            case CompanyRole.performer:
                                var newPerformer = new PerformerInCompany
                                {
                                    UserId = existingInvitation.User!.Id,
                                    CompanyId = existingInvitation.Company!.Id,
                                    JoinDate = DateTime.UtcNow,
                                    WorkLogs = new List<WorkLog>(),
                                    Salary = 0,
                                    WorkHours = 0,
                                    WorkDays = 0,
                                };

                                existingInvitation.Company!.Performers!.Add(newPerformer);
                                break;
                        }
                    }

                    break;

                case InvitationType.team:
                    existingInvitation.Team!.Members!.Add(existingInvitation.User);
                    break;
            }

            await _unitOfWork.SaveAsync();
            _unitOfWork.Invitations.Delete(existingInvitation);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
