﻿using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.UseCases
{
    public class RemoveResourceHandler : IRequestHandler<RemoveResourceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveResourceHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveResourceCommand request, CancellationToken cancellationToken)
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

            _unitOfWork.Resources.Delete(existingResource);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
