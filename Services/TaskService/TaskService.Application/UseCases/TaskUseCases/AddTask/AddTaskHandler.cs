using MediatR;
using TaskService.Application.UseCases;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;

public class AddTaskHandler : IRequestHandler<AddTaskCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessService _accessService;

    public AddTaskHandler(IUnitOfWork unitOfWork, IAccessService accessService)
    {
        _unitOfWork = unitOfWork;
        _accessService = accessService;
    }

    public async Task<Unit> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
            throw new ArgumentException("User have no permission");

        var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.CompanyId);
        if (existingCompany == null)
        {
            throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
        }

        BaseTaskInfo? parentTask = null;
        if (request.ParentId.HasValue)
        {
            parentTask = await _unitOfWork.TasksInfo.GetAsync(t => t.Id == request.ParentId);
            if (parentTask == null)
            {
                throw new ArgumentException($"Parent task with Id {request.ParentId} does not exist.");
            }
            if (parentTask.Company != existingCompany)
            {
                throw new ArgumentException($"Parent task with Id {request.ParentId} is not from Company with Id {request.CompanyId}.");
            }

            var childBudgets = parentTask.ChildTasks?.Sum(ct => ct.Budget) ?? 0;
            if (childBudgets + request.Budget > parentTask.Budget)
            {
                throw new InvalidOperationException($"The total budget for the task '{parentTask.Name}' exceeds the allocated amount.");
            }
        }

        var newTask = new BaseTaskInfo
        {
            Name = request.Name,
            Description = request.Description,
            CompanyId = request.CompanyId,
            ParentTask = parentTask,
            Budget = request.Budget,
            Status = TaskService.Domain.Enums.TaskStatus.todo,
            CompleteDate = null,
        };

        _unitOfWork.TasksInfo.Create(newTask);

        var taskData = new BaseTaskData
        {
            InfoId = newTask.Id,
            Resources = new List<Resource>()
        };

        _unitOfWork.TasksData.Create(taskData);

        await _unitOfWork.SaveAsync();

        newTask.Data = taskData;

        if (parentTask != null)
        {
            parentTask.ChildTasks ??= new List<BaseTaskInfo>();
            parentTask.ChildTasks.Add(newTask);
        }

        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
