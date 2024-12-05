using MediatR;
using TaskService.Application.DTOs;
using TaskService.Domain.Interfaces;
using AutoMapper;

namespace TaskService.Application.UseCases
{
    public class GetTaskDataQueryHandler : IRequestHandler<GetTaskDataQuery, TaskDataDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetTaskDataQueryHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<TaskDataDTO> Handle(GetTaskDataQuery request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");

            var taskInfo = existingCompany.Tasks!.FirstOrDefault(t => t.Id == request.taskId);
            if (taskInfo == null)
            {
                throw new ArgumentException($"Task with Id {request.taskId} not found for CompanyId {request.companyId}.");
            }

            var hasManagerAccess = await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!);
            var hasPerformerAccess = await _accessService.HavePerformerTaskAccessAsync(existingCompany.Id, taskInfo.Id, request.username!);

            if (!hasManagerAccess && !hasPerformerAccess)
            {
                throw new UnauthorizedAccessException("User does not have access to this task.");
            }

            var taskDataDTO = _mapper.Map<TaskDataDTO>(taskInfo);

            if (taskInfo.Data?.Resources != null)
            {
                taskDataDTO.Resources = _mapper.Map<List<ResourceDTO>>(taskInfo.Data.Resources);
            }

            if (taskInfo.ChildTasks != null)
            {
                taskDataDTO.ChildTasks = _mapper.Map<List<TaskInfoDTO>>(taskInfo.ChildTasks);
            }

            return taskDataDTO;
        }
    }
}
