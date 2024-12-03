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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var dateTimeChecker = new DateTimeChecker
            {
                CompanyId = request.CompanyId,
                Type = request.Type,
                Address = request.Address,
                Model = request.Model
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
