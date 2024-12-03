namespace UnionService.Application.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string>? Members { get; set; }
    }
}
