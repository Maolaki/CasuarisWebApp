using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddAccessPerformerHandler : IRequestHandler<AddAccessPerformerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddAccessPerformerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddAccessPerformerCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingAccess = existingCompany.Accesses?.FirstOrDefault(a => a.Id == request.accessId);
            if (existingAccess == null)
            {
                throw new ArgumentException($"Access with Id {request.accessId} does not exist.");
            }

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.userId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.userId} does not exist.");
            }

            if (existingAccess.Performers == null)
            {
                existingAccess.Performers = new List<User>();
            }

            if (!existingAccess.Performers.Any(p => p.Id == request.userId))
            {
                existingAccess.Performers.Add(existingUser);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new ArgumentException($"User with Id {request.userId} is already a performer for Access with Id {request.accessId}.");
            }

            return Unit.Value;
        }
    }
}
