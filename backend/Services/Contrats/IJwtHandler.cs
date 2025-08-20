using Entities;
using Entities.Dtos;
using System.Collections.Generic;
using System.Security.Claims;

namespace Services.Contrats
{
    public interface IJwtHandler
    {
        TokenDto CreateToken(ErpUser user, IEnumerable<string> roles);
        TokenDto RefreshToken(string accessToken, string refreshToken);
        bool ValidateToken(string token);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        bool IsTokenExpired(string token);
        void RevokeToken(string token);
        IEnumerable<string> GetUserRolesFromToken(string token);
    }
}
