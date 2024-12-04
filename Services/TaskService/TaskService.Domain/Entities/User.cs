using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskService.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }

        public virtual ICollection<Company>? CompaniesOwner { get; set; } = new HashSet<Company>();
        public virtual ICollection<Company>? CompaniesManager { get; set; } = new HashSet<Company>();
    }
}
