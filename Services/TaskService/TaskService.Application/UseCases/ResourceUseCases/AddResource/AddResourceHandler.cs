using MediatR;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskService.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace TaskService.Application.UseCases
{
    public class AddResourceHandler : IRequestHandler<AddResourceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddResourceHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddResourceCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingTaskInfo = existingCompany.Tasks?.FirstOrDefault(td => td.Id == request.taskInfoId);
            if (existingTaskInfo == null)
            {
                throw new ArgumentException($"Task with Id {request.taskInfoId} does not exist.");
            }

            var existingTaskData = existingTaskInfo.Data;
            if (existingTaskData == null)
            {
                throw new ArgumentException($"Task with Id {request.taskInfoId} don't have data.");
            }

            var newResource = new Resource
            {
                BaseTaskDataId = existingTaskData.Id,
                Type = (ResourceType)request.type!,
                ContentType = request.type == ResourceType.image || request.type == ResourceType.video ? request.imageFile!.ContentType : null,
                Data = request.type == ResourceType.text ? request.resourceData : null,
                DataBytes = request.type == ResourceType.image || request.type == ResourceType.video ? ConvertToByteArray(request.imageFile) : null
            };

            _unitOfWork.Resources.Create(newResource);

            await _unitOfWork.SaveAsync();

            existingTaskData.Resources ??= new List<Resource>();
            existingTaskData.Resources.Add(newResource);

            _unitOfWork.TasksData.Update(existingTaskData);

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
