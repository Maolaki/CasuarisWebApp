﻿using StatisticsService.Domain.Interfaces;

namespace StatisticsService.Application.Services
{
    public class AccessService : IAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> HaveOwnerAccessAsync(int CompanyId, string username)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var existingOwner = existingCompany.Owners?.FirstOrDefault(u => u.Username == username);
            if (existingCompany.Owners is null || existingOwner == null)
                return false;

            return true;
        }

        public async Task<bool> HaveManagerAccessAsync(int CompanyId, string username)
        {
            if (await HaveOwnerAccessAsync(CompanyId, username))
                return true;

            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var existingManager = existingCompany.Managers?.FirstOrDefault(u => u.Username == username);
            if (existingCompany.Managers is null || existingManager == null)
                return false;

            return true;
        }

        public async Task<bool> HavePerformerTaskAccessAsync(int CompanyId, int TaskInfoId, string username)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == CompanyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {CompanyId} does not exist.");

            var taskAccess = existingCompany.Accesses?.FirstOrDefault(a => a.TaskId == TaskInfoId);
            if (existingCompany.Accesses is null || taskAccess == null)
                throw new ArgumentException($"Company with Id {CompanyId} don't have Access with TaskId {TaskInfoId}.");

            if (taskAccess.Performers?.FirstOrDefault(p => p.Username == username) is not null)
                return true;

            return false;
        }
    }
}
