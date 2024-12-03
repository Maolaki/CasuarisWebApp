using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatisticsService.Domain.Entities
{
    public class WorkLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PerformerInCompanyId { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan HoursWorked { get; set; }

        public virtual PerformerInCompany? PerformerInCompany { get; set; }
    }
}
