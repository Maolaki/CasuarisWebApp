using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyWorkerHandler : IRequestHandler<RemoveCompanyWorkerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveCompanyWorkerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveCompanyWorkerCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            switch (request.Role)
            {
                case "Owner":
                    var ownerToRemove = existingCompany.Owners?.FirstOrDefault(u => u.Id == request.UserId);
                    if (existingCompany.Owners is not null && ownerToRemove != null)
                    {
                        existingCompany.Owners.Remove(ownerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.UserId} is not an owner in the company.");
                    }
                    break;

                case "Manager":
                    var managerToRemove = existingCompany.Managers?.FirstOrDefault(u => u.Id == request.UserId);
                    if (existingCompany.Managers is not null && managerToRemove != null)
                    {
                        existingCompany.Managers.Remove(managerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.UserId} is not a manager in the company.");
                    }
                    break;

                case "Performer":
                    var performerToRemove = existingCompany.Performers?.FirstOrDefault(u => u.UserId == request.UserId);
                    if (existingCompany.Performers is not null && performerToRemove != null)
                    {
                        existingCompany.Performers.Remove(performerToRemove);
                        _unitOfWork.Performers.Delete(performerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.UserId} is not a performer in the company.");
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid role. The valid roles are: 'Owner', 'Manager', or 'Performer'.");
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
