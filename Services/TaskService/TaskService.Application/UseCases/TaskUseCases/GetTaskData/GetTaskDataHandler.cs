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
            var hasManagerAccess = await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username);
            var hasPerformerAccess = await _accessService.CheckPerformerTaskAccessAsync(request.CompanyId, request.TaskId, request.username);

            if (!hasManagerAccess && !hasPerformerAccess)
            {
                throw new UnauthorizedAccessException("User does not have access to this task.");
            }

            var taskInfo = await _unitOfWork.TasksInfo.GetAsync(t => t.CompanyId == request.CompanyId && t.Id == request.TaskId);

            if (taskInfo == null)
            {
                throw new ArgumentException($"Task with Id {request.TaskId} not found for CompanyId {request.CompanyId}.");
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
