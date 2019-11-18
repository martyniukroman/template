using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Interfaces;
using hsl.db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace hsl.bl.Services
{
    public class TokenService : IToken
    {

        private readonly HslapiContext _hslapiContext;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly string JwtSecret = "bed77aaafefas5c57fc865fasf6c0a1e2533760"; //TODO: move jwt secret key to app settings

        public TokenService(
            HslapiContext hslapiContext, 
            IOptions<JwtIssuerOptions> options,
            UserManager<AppUser> userManager
        )
        {
            this._userManager = userManager;
            this._hslapiContext = hslapiContext;
            this._jwtIssuerOptions = options.Value;
        }
        
        public async Task<IActionResult> RefreshToken(TokenRequestModel model)
        {
            try
            {
                var refreshToken = _hslapiContext.Tokens.FirstOrDefault(x =>
                    x.ClientId == _jwtIssuerOptions.ClientId && x.Value == model.RefreshToken);

                if (refreshToken == null)
                    return new BadRequestObjectResult(new ErrorViewModel
                    {
                        code = 400,
                        caption = "Invalid refresh token, you are forced to Re-Login",
                        tag = "rTokenError",
                        afterAction = "relogin"
                    });
                if (refreshToken.ExpiryTime < DateTime.UtcNow)
                    return new BadRequestObjectResult(new ErrorViewModel
                    {
                        code = 400,
                        caption = "Token lifetime is expired, you are forced to Re-Login",
                        tag = "rTokenError",
                        afterAction = "relogin"
                    });

                var user = await _userManager.FindByIdAsync(refreshToken.UserId);

                if (user == null)
                    return new BadRequestObjectResult(new ErrorViewModel
                    {
                        code = 404,
                        caption = "User not found",
                        tag = "notFoundError"
                    });

                var newRefreshToken = CreateRefreshToken(refreshToken.ClientId, user.Id);
                _hslapiContext.Tokens.Remove(refreshToken);
                _hslapiContext.Tokens.Add(newRefreshToken);
                await _hslapiContext.SaveChangesAsync();

                var response = await CreateAccessToken(user, newRefreshToken.Value);
                return new OkObjectResult(new {authToken = response});
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorViewModel
                {
                    code = 500,
                    caption = e.Message + ' ' + e.InnerException?.Message,
                    tag = "exceptionError"
                });
            }
        }
        
        public async Task<IActionResult> GenerateNewToken(TokenRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
                user = await _userManager.FindByNameAsync(model.UserName);


            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //TODO: email validation isEmailConfirmed

                var newRefreshToken =
                    CreateRefreshToken(_jwtIssuerOptions.ClientId, user.Id); // client id might be null
                var oldRefreshTokens = _hslapiContext.Tokens.Where(x => x.UserId == user.Id);
                if (oldRefreshTokens != null)
                {
                    _hslapiContext.Tokens.RemoveRange(oldRefreshTokens);
                }

                _hslapiContext.Tokens.Add(newRefreshToken);
                await _hslapiContext.SaveChangesAsync();

                var accessToken = await CreateAccessToken(user, newRefreshToken.Value);
                return new OkObjectResult(new {authToken = accessToken});
            }

            return new BadRequestObjectResult(new ErrorViewModel
            {
                code = 404,
                caption = "Please Check the Login Credentials - Invalid Username/Password was entered",
                tag = "notFoundError"
            });
        }
        
        private async Task<TokenResponseModel> CreateAccessToken(AppUser appUser, string rToken)
        {
            double tokenExpiryTime = Convert.ToDouble(_jwtIssuerOptions.NotBefore); // token lifetime
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.JwtSecret));
            var roles = await _userManager.GetRolesAsync(appUser);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()), // not sure about validating role
                }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Issuer = _jwtIssuerOptions.Issuer,
                Audience = _jwtIssuerOptions.Audience,
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiryTime),
            };

            var access_token = tokenHandler.CreateToken(tokenDescriptor);
            var encoded_token = tokenHandler.WriteToken(access_token);
            return new TokenResponseModel()
            {
                token = encoded_token,
                expiration = access_token.ValidTo,
                refresh_token = rToken,
                roles = roles.FirstOrDefault(),
                username = appUser.UserName,
                displayName = appUser.DisplayName,
                userId = appUser.Id,
            };
        }
        
        private RefreshTokenModel CreateRefreshToken(string clientId, string userId)
        {
            return new RefreshTokenModel()
            {
                ClientId = clientId,
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddDays(1),
            };
        }
        
    }
}