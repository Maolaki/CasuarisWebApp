namespace AuthService.Application.DTOs
{
    public class AuthenticatedDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
