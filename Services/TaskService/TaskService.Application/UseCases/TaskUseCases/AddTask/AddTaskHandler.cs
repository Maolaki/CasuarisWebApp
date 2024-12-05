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
        var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
        if (existingCompany == null)
        {
            throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
        }

        if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
            throw new ArgumentException("User have no permission");

        BaseTaskInfo? parentTask = null;
        if (request.parentId.HasValue)
        {
            parentTask = await _unitOfWork.TasksInfo.GetAsync(t => t.Id == request.parentId);
            if (parentTask == null)
            {
                throw new ArgumentException($"Parent task with Id {request.parentId} does not exist.");
            }
            if (parentTask.Company != existingCompany)
            {
                throw new ArgumentException($"Parent task with Id {request.parentId} is not from Company with Id {request.companyId}.");
            }

            var childBudgets = parentTask.ChildTasks?.Sum(ct => ct.Budget) ?? 0;
            if (childBudgets + request.budget > parentTask.Budget)
            {
                throw new InvalidOperationException($"The total budget for the task '{parentTask.Name}' exceeds the allocated amount.");
            }
        }

        var newTask = new BaseTaskInfo
        {
            Name = request.name,
            Description = request.description,
            CompanyId = (int)request.companyId!,
            ParentTask = parentTask,
            Budget = (decimal)request.budget!,
            Status = TaskService.Domain.Enums.TaskStatus.todo,
            CompleteDate = null,
        };

        _unitOfWork.TasksInfo.Create(newTask);

        await _unitOfWork.SaveAsync();

        var taskData = new BaseTaskData
        {
            InfoId = newTask.Id
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

        var newAccess = new Access
        {
            CompanyId = existingCompany.Id,
            TaskId = newTask.Id
        };

        _unitOfWork.Accesses.Create(newAccess);

        return Unit.Value;
    }
}
