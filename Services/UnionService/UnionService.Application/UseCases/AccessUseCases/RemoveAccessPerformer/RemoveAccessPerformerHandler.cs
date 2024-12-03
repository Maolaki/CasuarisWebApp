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
            if (await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username))
                throw new ArgumentException("User have no permission");

            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingAccess = existingCompany.Accesses!.FirstOrDefault(a => a.Id == request.AccessId);
            if (existingAccess == null)
            {
                throw new ArgumentException($"Access with Id {request.AccessId} does not exist.");
            }

            if (existingAccess.Performers == null || !existingAccess.Performers.Any(p => p.Id == request.UserId))
            {
                throw new ArgumentException($"User with Id {request.UserId} is not a performer for Access with Id {request.AccessId}.");
            }

            var performerToRemove = existingAccess.Performers.First(p => p.Id == request.UserId);
            existingAccess.Performers.Remove(performerToRemove);

            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
