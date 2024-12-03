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
        var company = await _unitOfWork.Companies.GetAsync(c => c.Id == request.CompanyId);
        if (company == null)
            throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");

        bool isManagerAccess = await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username);

        IEnumerable<BaseTaskInfo> tasks;

        if (isManagerAccess)
        {
            tasks = company.Tasks ?? Enumerable.Empty<BaseTaskInfo>();
        }
        else
        {
            tasks = company.Accesses?
                .Where(a => a.Performers?.Any(p => p.Username == request.username) == true)
                .SelectMany(a => a.Company!.Tasks?
                    .Where(t => t.Id == a.TaskId && t.ParentId == 0) ?? Enumerable.Empty<BaseTaskInfo>())
                .ToList() ?? new List<BaseTaskInfo>();

        }

        return _mapper.Map<IEnumerable<TaskInfoDTO>>(tasks);
    }
}
