using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyDateTimeCheckerHandler : IRequestHandler<RemoveCompanyDateTimeCheckerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveCompanyDateTimeCheckerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveCompanyDateTimeCheckerCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckOwnerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingDateTimeChecker = existingCompany.DateTimeCheckers!.FirstOrDefault(dtc => dtc.Id == request.DateTimeCheckerId);
            if (existingDateTimeChecker == null)
            {
                throw new ArgumentException($"DateTimeChecker with Id {request.DateTimeCheckerId} does not exist.");
            }

            _unitOfWork.DateTimeCheckers.Delete(existingDateTimeChecker);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
