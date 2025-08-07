namespace Infrastructure.Configuration
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenLifetimeMinutes { get; set; }
        public string SecretKey { get; set; }
    }
}
