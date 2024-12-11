namespace TaskService.Application.DTOs
{
    public class TaskInfoDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
        public DateOnly? CompleteDate { get; set; }
        public UserDTO[]? Members { get; set; }
    }
}