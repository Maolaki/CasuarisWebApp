namespace TaskService.Domain.Interfaces
{
    public interface IAccessService
    {
        public Task<bool> HaveOwnerAccessAsync(int CompanyId, string username);
        public Task<bool> HaveManagerAccessAsync(int CompanyId, string username);
        public Task<bool> HavePerformerTaskAccessAsync(int CompanyId, int TaskInfoId, string username);
    }
}
