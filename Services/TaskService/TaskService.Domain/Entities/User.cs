using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskService.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }

        public virtual ICollection<Company>? Companies { get; set; }
    }
}
