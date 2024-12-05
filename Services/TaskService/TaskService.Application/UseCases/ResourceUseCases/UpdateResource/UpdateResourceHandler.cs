using MediatR;
using Microsoft.AspNetCore.Http;
using TaskService.Domain.Enums;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.UseCases
{
    public class UpdateResourceHandler : IRequestHandler<UpdateResourceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public UpdateResourceHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingResource = await _unitOfWork.Resources.GetAsync(r => r.Id == request.resourceId);
            if (existingResource == null)
            {
                throw new ArgumentException($"Resource with Id {request.resourceId} does not exist.");
            }

            var existingTaskData = existingResource.BaseTaskData;
            var existingTaskInfo = existingTaskData?.Info;
            if (existingTaskInfo == null || existingCompany.Tasks == null || !existingCompany.Tasks.Contains(existingTaskInfo))
                throw new ArgumentException("User have no permisson.");

            existingResource.Type = (ResourceType)request.type!;
            existingResource.ContentType = request.type == ResourceType.image || request.type == ResourceType.video ? request.imageFile!.ContentType : null;
            existingResource.Data = request.type == ResourceType.text ? request.data : null;
            existingResource.DataBytes = request.type == ResourceType.image || request.type == ResourceType.video ? ConvertToByteArray(request.imageFile) : null;

            _unitOfWork.Resources.Update(existingResource);

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
