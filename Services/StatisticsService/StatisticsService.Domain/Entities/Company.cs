using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StatisticsService.Domain.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? LogoContentType { get; set; }
        public byte[]? LogoData { get; set; }

        public virtual ICollection<User>? Owners { get; set; } = new HashSet<User>();
        public virtual ICollection<User>? Managers { get; set; } = new HashSet<User>();
        public virtual ICollection<PerformerInCompany>? Performers { get; set; } = new HashSet<PerformerInCompany>();
        public virtual ICollection<Team>? Teams { get; set; } = new HashSet<Team>();
        public virtual ICollection<BaseTaskInfo>? Tasks { get; set; } = new HashSet<BaseTaskInfo>();
        public virtual ICollection<Access>? Accesses { get; set; } = new HashSet<Access>();
    }
}
