using TaskService.Domain.Enums;

namespace TaskService.Application.DTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string? Data { get; set; }
        byte[]? DataBytes { get; set; }
        public string? ContentType { get; set; }
        public ResourceType Type { get; set; }
    }
}