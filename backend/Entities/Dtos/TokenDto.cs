using System.Security.Claims;

namespace Entities.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; } // seconds
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }

    public class TokenValidationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
        public ClaimsPrincipal? ClaimsPrincipal { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
