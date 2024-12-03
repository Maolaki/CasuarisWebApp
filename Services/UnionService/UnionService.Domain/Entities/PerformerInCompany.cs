using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnionService.Domain.Entities
{
    public class PerformerInCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int WorkHours { get; set; }
        public int WorkDays { get; set; }

        public virtual User? User { get; set; }
        public virtual Company? Company { get; set; }
        public virtual List<WorkLog>? WorkLogs { get; set; }
    }
}
