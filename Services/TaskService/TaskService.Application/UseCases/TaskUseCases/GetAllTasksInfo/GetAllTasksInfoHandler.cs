using MediatR;
using AutoMapper;
using TaskService.Domain.Interfaces;
using TaskService.Application.DTOs;
using TaskService.Application.UseCases;
using TaskService.Domain.Entities;

public class GetAllTasksInfoQueryHandler : IRequestHandler<GetAllTasksInfoQuery, IEnumerable<TaskInfoDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessService _accessService;
    private readonly IMapper _mapper;

    public GetAllTasksInfoQueryHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _accessService = accessService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskInfoDTO>> Handle(GetAllTasksInfoQuery request, CancellationToken cancellationToken)
    {
        var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
        if (existingCompany == null)
            throw new ArgumentException($"Company with Id {request.companyId} does not exist.");

        bool isManagerAccess = await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!);

        IEnumerable<BaseTaskInfo> tasks;

        if (isManagerAccess)
        {
            tasks = existingCompany.Tasks!;
        }
        else
        {
            tasks = existingCompany.Accesses?
                .Where(a => a.Performers?.Any(p => p.Username == request.username) == true)
                .SelectMany(a => a.Company!.Tasks?
                    .Where(t => t.Id == a.TaskId && t.ParentId == 0) ?? Enumerable.Empty<BaseTaskInfo>())
                .ToList() ?? new List<BaseTaskInfo>();
        }

        var taskInfoDtos = tasks.Select(task =>
        {
            var access = existingCompany.Accesses?.FirstOrDefault(a => a.TaskId == task.Id);
            var performers = access?.Performers ?? Enumerable.Empty<User>();
            var taskDto = _mapper.Map<TaskInfoDTO>(task);
            taskDto.Members = _mapper.Map<UserDTO[]>(performers);
            return taskDto;
        });

        return taskInfoDtos;
    }
}
