using MediatR;
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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingResource = await _unitOfWork.Resources.GetAsync(r => r.Id == request.ResourceId);
            if (existingResource == null)
            {
                throw new ArgumentException($"Resource with Id {request.ResourceId} does not exist.");
            }

            var existingTaskData = existingResource.BaseTaskData;
            var existingTaskInfo = existingTaskData?.Info;
            if (existingTaskInfo == null || existingCompany.Tasks == null || !existingCompany.Tasks.Contains(existingTaskInfo))
                throw new ArgumentException("User have no permisson.");

            existingResource.Type = request.Type;
            existingResource.ContentType = request.Type == ResourceType.image || request.Type == ResourceType.video ? request.ContentType : null;
            existingResource.Data = request.Type == ResourceType.text ? request.Data : null;
            existingResource.DataBytes = request.Type == ResourceType.image || request.Type == ResourceType.video ? request.DataBytes : null;

            _unitOfWork.Resources.Update(existingResource);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
