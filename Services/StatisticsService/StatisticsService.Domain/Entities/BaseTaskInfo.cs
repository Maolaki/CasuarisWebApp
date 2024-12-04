using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StatisticsService.Domain.Entities
{
    public class BaseTaskInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Budget { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public DateOnly? CompleteDate { get; set; }

        public virtual Company? Company { get; set; }
        public virtual BaseTaskInfo? ParentTask { get; set; }
        public virtual ICollection<BaseTaskInfo>? ChildTasks { get; set; } = new HashSet<BaseTaskInfo>();
    }
}
