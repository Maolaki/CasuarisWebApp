using UnionService.Domain.Enums;

namespace UnionService.Application.DTOs
{
    public class InvitationDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public CompanyRole? Role { get; set; }
        public int? TeamId { get; set; }
        public InvitationType Type { get; set; }
    }
}
