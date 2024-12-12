using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class UpdateCompanyMemberHandler : IRequestHandler<UpdateCompanyMemberCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public UpdateCompanyMemberHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(UpdateCompanyMemberCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingPerformer = await _unitOfWork.Performers.GetAsync(p => p.Id == request.memberId && p.CompanyId == request.companyId);
            if (existingPerformer == null)
            {
                throw new ArgumentException($"Performer with Id {request.memberId} does not exist.");
            }

            existingPerformer.Salary = request.salary ?? existingPerformer.Salary;
            existingPerformer.WorkHours = request.workHours ?? existingPerformer.WorkHours;
            existingPerformer.WorkDays = request.workDays ?? existingPerformer.WorkDays;

            _unitOfWork.Performers.Update(existingPerformer);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
