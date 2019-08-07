using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hsl.api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly TokenModel _tokenModel;
        private readonly hslapiContext _context;

        public TokenController(
            UserManager<User> userManager,
            IOptions<JwtIssuerOptions> jwtOptions,
            TokenModel tokenModel,
            hslapiContext context)
        {
            this._context = context;
            this._userManager = userManager;
            this._jwtOptions = jwtOptions.Value;
            this._tokenModel = tokenModel;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Auth([FromBody] TokenRequest tokenRequest)
        {
            if (tokenRequest == null)
            {
                return BadRequest(new {message = "Token Request is invalid"});
            }

            switch (tokenRequest.GrantType)
            {
                case "password": return await this.GenerateNewToken(tokenRequest);

                case "refresh_token": return await this.GenerateRefreshToken(tokenRequest);

                default: return Unauthorized();
            }
        }

        private async Task<IActionResult> GenerateNewToken(TokenRequest tokenRequest)
        {
//            var identity = await GetClaimsIdentity(creds.UserName, creds.Password);
//            if (identity == null)
//                return BadRequest(new {message = "Invalid credentials"});


            var user = _userManager.FindByNameAsync(tokenRequest.UserName);
            if (user != null && _userManager.CheckPasswordAsync(user, tokenRequest.Password))
            {
                var rToken = CreateRefreshToken("secret", user.Id);
                var oldRefreshToken = _context.Tokens.Where(x => x.UserId == user.Id);

                if (oldRefreshToken != null)
                {
                    foreach (var item in oldRefreshToken)
                    {
                        _context.Tokens.Remove(item);
                    }
                }

                _context.Tokens.Add(rToken);
                await _context.SaveChangesAsync();

            }
        }

        private async Task<IActionResult> GenerateRefreshToken(TokenRequest tokenRequest)
        {
        }

        private TokenModel CreateRefreshToken(string clientId, string userId)
        {
            return new TokenModel()
            {
                ClientId = clientId,
                UserId = userId,
                Token = Guid.NewGuid().ToString("N"),
                CratedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
            };
        }
        private TokenModel CreateAccessToken(string clientId, string userId)
        {
            return new TokenModel()
            {
                ClientId = clientId,
                UserId = userId,
                Token = Guid.NewGuid().ToString("N"),
                CratedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
            };
        }
    }
}
