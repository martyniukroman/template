using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Interfaces;
using hsl.api.Models;
using Newtonsoft.Json;

namespace hsl.api.Helpers
{
    public static class TokensHelper
    {
        public static async Task<string> GenerateJwtAsync(
            ClaimsIdentity identity,
            IJwtFactory jwtFactory,
            string userName,
            JwtIssuerOptions jwtIssuerOptions,
            JsonSerializerSettings jsonSerializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int) jwtIssuerOptions.ValidFor.TotalSeconds
            };
            return JsonConvert.SerializeObject(response, jsonSerializerSettings);
        }
    }
}
