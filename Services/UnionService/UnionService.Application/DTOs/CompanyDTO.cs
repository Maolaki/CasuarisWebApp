namespace UnionService.Application.DTOs
{
    public class CompanyDTO
    {
        int Id { get; set; }
        string? Name { get; set; }
        string? Description { get; set; }
        string? LogoContentType { get; set; }
        byte[]? LogoData { get; set; }
    }
}
