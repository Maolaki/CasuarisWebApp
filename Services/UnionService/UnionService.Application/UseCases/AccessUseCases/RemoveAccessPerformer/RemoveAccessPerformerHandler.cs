using MediatR;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class RemoveAccessPerformerHandler : IRequestHandler<RemoveAccessPerformerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public RemoveAccessPerformerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(RemoveAccessPerformerCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingAccess = existingCompany.Accesses!.FirstOrDefault(a => a.Id == request.accessId);
            if (existingAccess == null)
            {
                throw new ArgumentException($"Access with Id {request.accessId} does not exist.");
            }

            if (existingAccess.Performers == null || !existingAccess.Performers.Any(p => p.Id == request.userId))
            {
                throw new ArgumentException($"User with Id {request.userId} is not a performer for Access with Id {request.accessId}.");
            }

            var performerToRemove = existingAccess.Performers.First(p => p.Id == request.userId);
            existingAccess.Performers.Remove(performerToRemove);

            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
