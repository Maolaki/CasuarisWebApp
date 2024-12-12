using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddWorkLogHandler : IRequestHandler<AddWorkLogCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddWorkLogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddWorkLogCommand request, CancellationToken cancellationToken)
        {
            var performerInCompany = await _unitOfWork.Performers.GetAsync(
                p => p.User!.Username == request.username && p.CompanyId == request.companyId);

            if (performerInCompany == null)
            {
                throw new ArgumentException("User is not associated with the given company.");
            }

            var workLog = new WorkLog
            {
                PerformerInCompanyId = performerInCompany.Id,
                WorkDate = (DateTime)request.workDate!,
                HoursWorked = (TimeSpan)request.hoursWorked!
            };

            _unitOfWork.WorkLogs.Create(workLog);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
