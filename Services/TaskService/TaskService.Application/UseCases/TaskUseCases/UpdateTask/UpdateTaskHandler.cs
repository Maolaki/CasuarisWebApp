using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.UseCases
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public UpdateTaskHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingTask = await _unitOfWork.TasksInfo.GetAsync(t => t.Id == request.taskId);
            if (existingTask == null)
            {
                throw new ArgumentException($"Task with Id {request.taskId} does not exist.");
            }

            if (existingCompany.Tasks == null || !existingCompany.Tasks.Contains(existingTask))
                throw new ArgumentException("User have no permisson.");

            existingTask.Name = request.name ?? existingTask.Name;
            existingTask.Description = request.description ?? existingTask.Description;

            if (request.budget.HasValue)
            {
                var childBudgets = existingTask.ChildTasks?.Sum(ct => ct.Budget) ?? 0;
                if (childBudgets > request.budget.Value)
                {
                    throw new InvalidOperationException($"The total budget for child tasks exceeds the new budget.");
                }

                existingTask.Budget = request.budget.Value;
            }

            if (request.status.HasValue)
            {
                if (request.status.Value == Domain.Enums.TaskStatus.done)
                {
                    existingTask.Status = Domain.Enums.TaskStatus.done;
                    existingTask.CompleteDate = DateOnly.FromDateTime(DateTime.UtcNow);

                    if (existingTask.ChildTasks != null)
                    {
                        foreach (var childTask in existingTask.ChildTasks)
                        {
                            childTask.Status = Domain.Enums.TaskStatus.done;
                            childTask.CompleteDate = DateOnly.FromDateTime(DateTime.UtcNow);
                        }
                    }
                }
                else
                {
                    existingTask.Status = request.status.Value;
                    if (existingTask.CompleteDate != null)
                        existingTask.CompleteDate = null;
                }
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
