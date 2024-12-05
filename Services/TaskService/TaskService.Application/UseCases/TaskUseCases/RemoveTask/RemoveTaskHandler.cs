using MediatR;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.UseCases
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTaskCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveTaskHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
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

            RemoveTask(existingTask);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }

        private void RemoveTask(BaseTaskInfo task)
        {
            foreach (var childTask in task.ChildTasks!)
                RemoveTask(childTask);

            if (task.Data != null)
                _unitOfWork.TasksData.Delete(task.Data);

            _unitOfWork.TasksInfo.Delete(task);
        }
    }
}
