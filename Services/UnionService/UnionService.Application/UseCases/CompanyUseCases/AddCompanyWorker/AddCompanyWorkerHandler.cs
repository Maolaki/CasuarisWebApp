using MediatR;
using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class AddCompanyWorkerHandler : IRequestHandler<AddCompanyWorkerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCompanyWorkerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddCompanyWorkerCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.CompanyId} does not exist.");
            }

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with Id {request.UserId} does not exist.");
            }

            switch (request.Role)
            {
                case "Owner":
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

                case "Manager":
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

                case "Performer":
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
                        Salary = request.Salary ?? 0,
                        WorkHours = request.WorkHours ?? 0,
                        WorkDays = request.WorkDays ?? 0,
                    };

                    _unitOfWork.Performers.Create(newPerformer);
                    existingCompany.Performers.Add(newPerformer);
                    break;

                default:
                    throw new ArgumentException($"Invalid role: {request.Role}. Allowed roles: Owner, Manager, Performer.");
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
