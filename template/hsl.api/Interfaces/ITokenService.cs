using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using hsl.api.Models;

namespace hsl.api.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateAccessToken(User user);
        RefreshTokenModel GenerateRefreshToken(User user);
        RefreshTokenModel RefreshTokenValidation(string token);
    }
}
