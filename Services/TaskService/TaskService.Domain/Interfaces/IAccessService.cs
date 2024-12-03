namespace TaskService.Domain.Interfaces
{
    public interface IAccessService
    {
        public Task<bool> CheckOwnerAccessAsync(int CompanyId, string username);
        public Task<bool> CheckManagerAccessAsync(int CompanyId, string username);
        public Task<bool> CheckPerformerTaskAccessAsync(int CompanyId, int TaskInfoId, string username);
    }
}
