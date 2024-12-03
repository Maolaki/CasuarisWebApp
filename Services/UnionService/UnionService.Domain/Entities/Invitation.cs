using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnionService.Domain.Enums;

namespace UnionService.Domain.Entities
{
    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int TeamId {  get; set; }
        public InvitationType Type { get; set; }

        public virtual User? User { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Team? Team { get; set; }
    }
}
