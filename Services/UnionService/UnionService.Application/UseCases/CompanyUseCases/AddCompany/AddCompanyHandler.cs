using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddCompanyHandler : IRequestHandler<AddCompanyCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = request.Name,
                Description = request.Description,
                LogoContentType = request.LogoContentType ?? null,
                LogoData = request.LogoData ?? null,
                Owners = new List<User>(),
                Managers = new List<User>(),
                Performers = new List<PerformerInCompany>(),
                Teams = new List<Team>(),
                Accesses = new List<Access>(),
                Tasks = new List<BaseTaskInfo>(),
                DateTimeCheckers = new List<DateTimeChecker>()
            };

            var user = await _unitOfWork.Users.GetAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                throw new ArgumentException($"User with Id {request.UserId} does not exist.");
            }
            company.Owners.Add(user);

            _unitOfWork.Companies.Create(company);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
