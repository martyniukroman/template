using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Models;

namespace hsl.api.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateAccessToken(User user);
    }
}
