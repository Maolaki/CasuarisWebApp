using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UnionService.Domain.Enums;

namespace UnionService.Domain.Entities
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BaseTaskDataId { get; set; }
        public string? Data { get; set; }
        public byte[]? DataBytes { get; set; }
        public string? ContentType { get; set; }
        public ResourceType Type { get; set; }

        public virtual BaseTaskData? BaseTaskData { get; set; }
    }
}
