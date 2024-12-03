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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingTask = await _unitOfWork.TasksInfo.GetAsync(t => t.Id == request.TaskId);
            if (existingTask == null)
            {
                throw new ArgumentException($"Task with Id {request.TaskId} does not exist.");
            }

            if (existingCompany.Tasks == null || !existingCompany.Tasks.Contains(existingTask))
                throw new ArgumentException("User have no permisson.");

            existingTask.Name = request.Name ?? existingTask.Name;
            existingTask.Description = request.Description ?? existingTask.Description;

            if (request.Budget.HasValue)
            {
                var childBudgets = existingTask.ChildTasks?.Sum(ct => ct.Budget) ?? 0;
                if (childBudgets > request.Budget.Value)
                {
                    throw new InvalidOperationException($"The total budget for child tasks exceeds the new budget.");
                }

                existingTask.Budget = request.Budget.Value;
            }

            if (request.Status.HasValue)
            {
                if (request.Status.Value == Domain.Enums.TaskStatus.done)
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
                    existingTask.Status = request.Status.Value;
                    if (existingTask.CompleteDate != null)
                        existingTask.CompleteDate = null;
                }
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
