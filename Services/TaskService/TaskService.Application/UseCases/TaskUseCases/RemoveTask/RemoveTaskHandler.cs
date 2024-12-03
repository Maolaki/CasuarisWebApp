using MediatR;
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

            _unitOfWork.TasksInfo.Delete(existingTask);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
