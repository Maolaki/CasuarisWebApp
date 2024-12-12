using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Enums;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddCompanyWorkerHandler : IRequestHandler<AddCompanyWorkerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddCompanyWorkerHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddCompanyWorkerCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Username == request.memberUsername);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.memberUsername} does not exist.");
            }

            switch (request.role)
            {
                case CompanyRole.owner:
                    if (existingCompany.Owners == null)
                        existingCompany.Owners = new List<User>();

                    if (existingCompany.Managers != null && existingCompany.Managers.Contains(existingUser))
                    {
                        existingCompany.Managers.Remove(existingUser);
                    }

                    if (existingCompany.Performers != null && existingCompany.Performers.Any(p => p.UserId == existingUser.Id))
                    {
                        var performerToRemove = existingCompany.Performers.FirstOrDefault(p => p.UserId == existingUser.Id);
                        if (performerToRemove != null)
                        {
                            existingCompany.Performers.Remove(performerToRemove);
                            _unitOfWork.Performers.Delete(performerToRemove);
                        }
                    }

                    if (!existingCompany.Owners.Contains(existingUser))
                        existingCompany.Owners.Add(existingUser);
                    break;

                case CompanyRole.manager:
                    if (existingCompany.Managers == null)
                        existingCompany.Managers = new List<User>();

                    if (existingCompany.Owners != null && existingCompany.Owners.Contains(existingUser))
                    {
                        existingCompany.Owners.Remove(existingUser);
                    }

                    if (existingCompany.Performers != null && existingCompany.Performers.Any(p => p.UserId == existingUser.Id))
                    {
                        var performerToRemove = existingCompany.Performers.FirstOrDefault(p => p.UserId == existingUser.Id);
                        if (performerToRemove != null)
                        {
                            existingCompany.Performers.Remove(performerToRemove);
                            _unitOfWork.Performers.Delete(performerToRemove);
                        }
                    }

                    if (!existingCompany.Managers.Contains(existingUser))
                        existingCompany.Managers.Add(existingUser);
                    break;

                case CompanyRole.performer:
                    if (existingCompany.Performers == null)
                        existingCompany.Performers = new List<PerformerInCompany>();

                    if (existingCompany.Performers.Any(p => p.UserId == existingUser.Id))
                    {
                        throw new ArgumentException($"User with Id {existingUser.Id} already is Performer in company.");
                    }

                    if (existingCompany.Owners != null && existingCompany.Owners.Contains(existingUser))
                    {
                        existingCompany.Owners.Remove(existingUser);
                    }

                    if (existingCompany.Managers != null && existingCompany.Managers.Contains(existingUser))
                    {
                        existingCompany.Managers.Remove(existingUser);
                    }

                    var newPerformer = new PerformerInCompany
                    {
                        UserId = existingUser.Id,
                        CompanyId = existingCompany.Id,
                        JoinDate = DateTime.UtcNow,
                        WorkLogs = new List<WorkLog>(),
                        Salary = request.salary ?? 0,
                        WorkHours = request.workHours ?? 0,
                        WorkDays = request.workDays ?? 0,
                    };

                    _unitOfWork.Performers.Create(newPerformer);
                    existingCompany.Performers.Add(newPerformer);
                    break;
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
