using Microsoft.AspNetCore.Http;

namespace UnionService.Application.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? LogoFile { get; set; }
    }
}
