namespace TaskService.Application.DTOs
{
    public class TaskDataDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
        public DateOnly? CompleteDate { get; set; }
        public UserDTO[]? Members { get; set; }

        public ICollection<ResourceDTO>? Resources { get; set; }
        public ICollection<TaskInfoDTO>? ChildTasks { get; set; }
    }
}