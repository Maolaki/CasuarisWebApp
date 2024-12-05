using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddCompanyDateTimeCheckerHandler : IRequestHandler<AddCompanyDateTimeCheckerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddCompanyDateTimeCheckerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddCompanyDateTimeCheckerCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var dateTimeChecker = new DateTimeChecker
            {
                CompanyId = (int)request.companyId!,
                Type = (Domain.Enums.DateTimeCheckerType)request.type!,
                Address = request.address,
                Model = request.model
            };

            _unitOfWork.DateTimeCheckers.Create(dateTimeChecker);
            await _unitOfWork.SaveAsync();

            if (existingCompany.DateTimeCheckers == null)
            {
                existingCompany.DateTimeCheckers = new List<DateTimeChecker>();
            }

            existingCompany.DateTimeCheckers.Add(dateTimeChecker);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
