using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskService.Domain.Enums;

namespace TaskService.Domain.Entities
{
    public class BaseTaskData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InfoId { get; set; }

        public virtual BaseTaskInfo? Info { get; set; }
        public virtual ICollection<Resource>? Resources { get; set; }
    }
}
