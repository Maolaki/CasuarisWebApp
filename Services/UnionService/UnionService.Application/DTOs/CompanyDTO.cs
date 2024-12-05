using Microsoft.AspNetCore.Http;

namespace UnionService.Application.DTOs
{
    public class CompanyDTO
    {
        int Id { get; set; }
        string? Name { get; set; }
        string? Description { get; set; }
        public IFormFile? LogoFile { get; set; }
    }
}
