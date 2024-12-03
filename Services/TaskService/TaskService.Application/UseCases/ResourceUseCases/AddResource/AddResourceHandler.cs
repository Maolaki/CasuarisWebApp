using MediatR;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskService.Domain.Enums;

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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingTaskInfo = existingCompany.Tasks?.FirstOrDefault(td => td.Id == request.TaskInfoId);
            if (existingTaskInfo == null)
            {
                throw new ArgumentException($"Task with Id {request.TaskInfoId} does not exist.");
            }

            var existingTaskData = existingTaskInfo.Data;
            if (existingTaskData == null)
            {
                throw new ArgumentException($"Task with Id {request.TaskInfoId} don't have data.");
            }

            var newResource = new Resource
            {
                Type = request.Type,
                ContentType = request.Type == ResourceType.image || request.Type == ResourceType.video ? request.ContentType : null,
                Data = request.Type == ResourceType.text ? request.ResourceData : null,
                DataBytes = request.Type == ResourceType.image || request.Type == ResourceType.video ? request.ResourceDataBytes : null
            };

            _unitOfWork.Resources.Create(newResource);

            await _unitOfWork.SaveAsync();

            existingTaskData.Resources ??= new List<Resource>();
            existingTaskData.Resources.Add(newResource);

            _unitOfWork.TasksData.Update(existingTaskData);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
