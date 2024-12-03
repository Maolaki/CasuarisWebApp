using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public UpdateCompanyHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _accessService.CheckOwnerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            existingCompany.Name = request.Name ?? existingCompany.Name;
            existingCompany.Description = request.Description ?? existingCompany.Description;
            existingCompany.LogoContentType = request.LogoContentType ?? existingCompany.LogoContentType;
            existingCompany.LogoData = request.LogoData ?? existingCompany.LogoData;

            _unitOfWork.Companies.Update(existingCompany);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }

    }
}
