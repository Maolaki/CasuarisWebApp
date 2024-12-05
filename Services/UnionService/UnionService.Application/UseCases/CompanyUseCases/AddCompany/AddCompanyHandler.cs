using MediatR;
using Microsoft.AspNetCore.Http;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddCompanyHandler : IRequestHandler<AddCompanyCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = request.name,
                Description = request.description,
                LogoContentType = request.imageFile?.ContentType,
                LogoData = ConvertToByteArray(request.imageFile)
            };

            var user = await _unitOfWork.Users.GetAsync(u => u.Id == request.userId);
            if (user == null)
            {
                throw new ArgumentException($"User with Id {request.userId} does not exist.");
            }

            _unitOfWork.Companies.Create(company);

            await _unitOfWork.SaveAsync();

            company.Owners!.Add(user);

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
