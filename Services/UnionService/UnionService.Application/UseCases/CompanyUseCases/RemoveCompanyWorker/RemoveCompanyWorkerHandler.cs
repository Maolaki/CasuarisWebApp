using MediatR;
using UnionService.Domain.Enums;
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
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            switch (request.role)
            {
                case CompanyRole.owner:
                    var ownerToRemove = existingCompany.Owners?.FirstOrDefault(u => u.Id == request.userId);
                    if (existingCompany.Owners is not null && ownerToRemove != null)
                    {
                        existingCompany.Owners.Remove(ownerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.userId} is not an owner in the company.");
                    }
                    break;

                case CompanyRole.manager:
                    var managerToRemove = existingCompany.Managers?.FirstOrDefault(u => u.Id == request.userId);
                    if (existingCompany.Managers is not null && managerToRemove != null)
                    {
                        existingCompany.Managers.Remove(managerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.userId} is not a manager in the company.");
                    }
                    break;

                case CompanyRole.performer:
                    var performerToRemove = existingCompany.Performers?.FirstOrDefault(u => u.Id == request.userId);
                    if (existingCompany.Performers is not null && performerToRemove != null)
                    {
                        existingCompany.Performers.Remove(performerToRemove);
                        _unitOfWork.Performers.Delete(performerToRemove);
                    }
                    else
                    {
                        throw new ArgumentException($"User with Id {request.userId} is not a performer in the company.");
                    }
                    break;
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
