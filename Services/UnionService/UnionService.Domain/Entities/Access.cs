using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnionService.Domain.Entities
{
    public class Access
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int TaskId { get; set; }
        
        public virtual Company? Company { get; set; }
        public virtual List<User>? Performers { get; set; }
    }
}
