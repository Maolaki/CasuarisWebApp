using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyHandler : IRequestHandler<RemoveCompanyCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveCompanyHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckOwnerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");

            _unitOfWork.Companies.Delete(existingCompany);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
