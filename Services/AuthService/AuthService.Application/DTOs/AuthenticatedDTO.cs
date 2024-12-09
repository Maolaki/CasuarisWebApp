namespace AuthService.Application.DTOs
{
    public class AuthenticatedDTO
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
