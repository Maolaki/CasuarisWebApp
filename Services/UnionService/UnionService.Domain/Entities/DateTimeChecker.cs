using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UnionService.Domain.Enums;

namespace UnionService.Domain.Entities
{
    public class DateTimeChecker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTimeCheckerType Type { get; set; }
        public string? Address { get; set; }
        public string? Model { get; set; }

        public virtual Company? Company { get; set; }
    }
}
