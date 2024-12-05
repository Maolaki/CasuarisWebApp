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
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");

            if (!await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            _unitOfWork.Companies.Delete(existingCompany);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
