using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StatisticsService.Domain.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ICollection<User>? Members { get; set; }
    }
}
