using MediatR;
using Microsoft.AspNetCore.Http;
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
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            existingCompany.Name = request.name ?? existingCompany.Name;
            existingCompany.Description = request.description ?? existingCompany.Description;
            existingCompany.LogoContentType = request.imageFile?.ContentType ?? existingCompany.LogoContentType;
            existingCompany.LogoData = ConvertToByteArray(request.imageFile) ?? existingCompany.LogoData;

            _unitOfWork.Companies.Update(existingCompany);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }

        private byte[]? ConvertToByteArray(IFormFile? file)
        {
            if (file is null)
                return null;

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
