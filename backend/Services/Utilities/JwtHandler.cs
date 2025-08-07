using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Entities;
using Entities.Dtos;
using Services.Contrats;

namespace Services.Utilities
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly SigningCredentials _signingCredentials;
        private readonly IMemoryCache _cache;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IConfiguration configuration, IMemoryCache cache)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _cache = cache;
            _tokenHandler = new JwtSecurityTokenHandler();
            _signingCredentials = GetSigningCredentials();
            _tokenValidationParameters = GetTokenValidationParameters();
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _jwtSettings["securityKey"];
            if (string.IsNullOrEmpty(key))
                throw new Exception("JWT securityKey değeri bulunamadı");

            // Minimum 256-bit key güvenliği
            if (key.Length < 32)
                throw new Exception("JWT securityKey en az 32 karakter olmalıdır");

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings["validIssuer"],
                ValidAudience = _jwtSettings["validAudience"],
                IssuerSigningKey = _signingCredentials.Key,
                ClockSkew = TimeSpan.Zero, // Tam zaman kontrolü
                RequireExpirationTime = true
            };
        }
        //Token oluştururken kullanılacak claimleri oluşturur.Bu claimler kullanıcının bilgilerini içerir.
        private List<Claim> GetClaims(ErpUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("userId", user.Id),
                new Claim("userName", user.Email ?? string.Empty),
                new Claim("jti", Guid.NewGuid().ToString()), // Unique token ID
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Rolleri ekle
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateAccessToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiresInMinutes = Convert.ToDouble(_jwtSettings["accessTokenExpiresInMinutes"] ?? "15");
            
            return new JwtSecurityToken
            (
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public TokenDto CreateToken(ErpUser user, IEnumerable<string> roles)
        {
            var claims = GetClaims(user, roles);
            var accessToken = GenerateAccessToken(_signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();
            
            var accessTokenString = _tokenHandler.WriteToken(accessToken);
            var refreshTokenExpiresInDays = Convert.ToInt32(_jwtSettings["refreshTokenExpiresInDays"] ?? "7");

            var tokenDto = new TokenDto
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessToken.ValidTo,
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenExpiresInDays),
                TokenType = "Bearer",
                ExpiresIn = (int)(accessToken.ValidTo - DateTime.UtcNow).TotalSeconds,
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Roles = roles
            };

            // Refresh token'ı cache'e kaydet
            var cacheKey = $"refresh_token_{refreshToken}";
            _cache.Set(cacheKey, tokenDto, TimeSpan.FromDays(refreshTokenExpiresInDays));

            return tokenDto;
        }

        public TokenDto RefreshToken(string accessToken, string refreshToken)
        {
            // Refresh token'ı cache'den kontrol et
            var cacheKey = $"refresh_token_{refreshToken}";
            if (!_cache.TryGetValue(cacheKey, out TokenDto? cachedToken) || cachedToken == null)
                throw new UnauthorizedAccessException("Geçersiz refresh token");

            // Access token'ı validate et
            if (!ValidateToken(accessToken))
                throw new UnauthorizedAccessException("Geçersiz access token");

            // Refresh token süresi kontrolü
            if (cachedToken.RefreshTokenExpiration < DateTime.UtcNow)
            {
                _cache.Remove(cacheKey);
                throw new UnauthorizedAccessException("Refresh token süresi dolmuş");
            }

            // Yeni token oluştur
            var claimsPrincipal = GetPrincipalFromToken(accessToken);
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Token'da kullanıcı bilgisi bulunamadı");

            // Eski refresh token'ı cache'den sil
            _cache.Remove(cacheKey);

            // Yeni token oluştur (user bilgisi olmadan)
            var newClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName ?? string.Empty),
                new Claim("jti", Guid.NewGuid().ToString()),
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var role in roles)
            {
                newClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var newAccessToken = GenerateAccessToken(_signingCredentials, newClaims);
            var newRefreshToken = GenerateRefreshToken();
            var newRefreshTokenExpiresInDays = Convert.ToInt32(_jwtSettings["refreshTokenExpiresInDays"] ?? "7");

            var newTokenDto = new TokenDto
            {
                AccessToken = _tokenHandler.WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                AccessTokenExpiration = newAccessToken.ValidTo,
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(newRefreshTokenExpiresInDays),
                TokenType = "Bearer",
                ExpiresIn = (int)(newAccessToken.ValidTo - DateTime.UtcNow).TotalSeconds,
                UserId = userId,
                UserName = userName ?? string.Empty,
                Roles = roles
            };

            // Yeni refresh token'ı cache'e kaydet
            var newCacheKey = $"refresh_token_{newRefreshToken}";
            _cache.Set(newCacheKey, newTokenDto, TimeSpan.FromDays(newRefreshTokenExpiresInDays));

            return newTokenDto;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                // Blacklist kontrolü
                if (IsTokenBlacklisted(token))
                    return false;

                var principal = _tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);
                return principal != null;
            }
            catch
            {
                return false;
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                return _tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);
            }
            catch
            {
                throw new UnauthorizedAccessException("Geçersiz token");
            }
        }

        public bool IsTokenExpired(string token)
        {
            try
            {
                var jwtToken = _tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        public void RevokeToken(string token)
        {
            // Token'ı blacklist'e ekle
            var jti = GetTokenJti(token);
            if (!string.IsNullOrEmpty(jti))
            {
                var cacheKey = $"blacklist_{jti}";
                var expiresAt = GetTokenExpiration(token);
                if (expiresAt.HasValue)
                {
                    _cache.Set(cacheKey, true, expiresAt.Value - DateTime.UtcNow);
                }
            }
        }

        public IEnumerable<string> GetUserRolesFromToken(string token)
        {
            try
            {
                var principal = GetPrincipalFromToken(token);
                return principal.FindAll(ClaimTypes.Role).Select(c => c.Value);
            }
            catch
            {
                return new List<string>();
            }
        }

        private bool IsTokenBlacklisted(string token)
        {
            var jti = GetTokenJti(token);
            if (string.IsNullOrEmpty(jti))
                return false;

            var cacheKey = $"blacklist_{jti}";
            return _cache.TryGetValue(cacheKey, out _);
        }

        private string? GetTokenJti(string token)
        {
            try
            {
                var jwtToken = _tokenHandler.ReadJwtToken(token);
                return jwtToken.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            }
            catch
            {
                return null;
            }
        }

        private DateTime? GetTokenExpiration(string token)
        {
            try
            {
                var jwtToken = _tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo;
            }
            catch
            {
                return null;
            }
        }
    }
}
