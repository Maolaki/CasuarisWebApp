using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UnionService.Domain.Entities
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

        public virtual ICollection<User>? Owners { get; set; }
        public virtual ICollection<User>? Managers { get; set; }
        public virtual ICollection<PerformerInCompany>? Performers { get; set; }
        public virtual ICollection<Team>? Teams { get; set; }
        public virtual ICollection<BaseTaskInfo>? Tasks { get; set; }
        public virtual ICollection<Access>? Accesses { get; set; }
        public virtual ICollection<DateTimeChecker>? DateTimeCheckers { get; set; }
    }
}
