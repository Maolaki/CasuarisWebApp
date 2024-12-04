using UnionService.Domain.Interfaces;

namespace UnionService.Application.Services
{
    public class AccessService : IAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckOwnerAccessAsync(int CompanyId, string username)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var existingOwner = existingCompany.Owners?.FirstOrDefault(u => u.Username == username);
            if (existingCompany.Owners is null || existingOwner == null)
                return true;

            return false;
        }

        public async Task<bool> CheckManagerAccessAsync(int CompanyId, string username)
        {
            if (!await CheckOwnerAccessAsync(CompanyId, username))
                return false;

            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var existingManager = existingCompany.Managers?.FirstOrDefault(u => u.Username == username);
            if (existingCompany.Managers is null || existingManager == null)
                return true;

            return false;
        }

        public async Task<bool> CheckPerformerTaskAccessAsync(int CompanyId, int TaskInfoId, string username)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var taskAccess = existingCompany.Accesses?.FirstOrDefault(a => a.TaskId == TaskInfoId);
            if (existingCompany.Accesses is null || taskAccess == null)
                throw new ArgumentException($"Company with Id {CompanyId} don't have Access with TaskId {TaskInfoId}.");

            if (taskAccess.Performers?.FirstOrDefault(p => p.Username == username) is not null)
                return false;

            return true;
        }
    }
}
