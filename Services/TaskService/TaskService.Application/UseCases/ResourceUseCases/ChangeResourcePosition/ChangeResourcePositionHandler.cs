using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.UseCases
{
    public class ChangeResourcePositionHandler : IRequestHandler<ChangeResourcePositionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public ChangeResourcePositionHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(ChangeResourcePositionCommand request, CancellationToken cancellationToken)
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

            var existingResource = existingTaskData.Resources?.FirstOrDefault(r => r.Id == request.resourceId);
            if (existingResource == null)
            {
                throw new ArgumentException($"Resource with Id {request.resourceId} does not exist in Data of TaskInfo {request.taskInfoId}.");
            }

            if (request.newPosition >= existingTaskData.Resources!.Count)
            {
                throw new ArgumentException($"New position {request.newPosition} is out of bounds.");
            }

            var resourceList = existingTaskData.Resources.ToList();

            resourceList.Remove(existingResource);

            resourceList.Insert((int)request.newPosition!, existingResource);

            existingTaskData.Resources = resourceList;

            _unitOfWork.TasksData.Update(existingTaskData);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
