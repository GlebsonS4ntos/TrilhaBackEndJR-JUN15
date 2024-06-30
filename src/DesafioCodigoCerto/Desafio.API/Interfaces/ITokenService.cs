using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio.API.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(IEnumerable<Claim> clains);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalClaimsFromExpiredToken(string token);
    }
}
