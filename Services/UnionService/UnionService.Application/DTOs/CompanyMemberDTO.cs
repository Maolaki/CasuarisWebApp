using UnionService.Domain.Enums;

namespace UnionService.Application.DTOs
{
    public class CompanyMemberDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime JoinDate { get; set; }
        public CompanyRole CompanyRole { get; set; }
        public decimal Salary { get; set; }
        public int WorkHours { get; set; }
        public int WorkDays { get; set; }
    }
}
