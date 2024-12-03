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

            var existingResource = existingTaskData.Resources?.FirstOrDefault(r => r.Id == request.ResourceId);
            if (existingResource == null)
            {
                throw new ArgumentException($"Resource with Id {request.ResourceId} does not exist in Data of TaskInfo {request.TaskInfoId}.");
            }

            if (request.NewPosition >= existingTaskData.Resources!.Count)
            {
                throw new ArgumentException($"New position {request.NewPosition} is out of bounds.");
            }

            var resourceList = existingTaskData.Resources.ToList();

            resourceList.Remove(existingResource);

            resourceList.Insert(request.NewPosition, existingResource);

            existingTaskData.Resources = resourceList;

            _unitOfWork.TasksData.Update(existingTaskData);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
