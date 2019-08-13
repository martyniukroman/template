using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace hsl.api.Controllers
{
    public class RToken
    {
        public string RefreshToken { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly RefreshTokenModel _refreshTokenModel;
        private readonly UserManager<User> _userManager;
        private readonly hslapiContext _hslapiContext;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public TokenController(UserManager<User> userManager,
            ITokenService tokenService, RefreshTokenModel refreshTokenModel, hslapiContext hslapiContextMir,
            IOptions<JwtIssuerOptions> options)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenModel = refreshTokenModel;
            _hslapiContext = hslapiContextMir;
            _jwtIssuerOptions = options.Value;
        }

        [HttpPost("action")]
        public async Task<IActionResult> Post([FromBody] TokenRequestModel model)
        {
            if (model == null)
            {
                return new NotFoundObjectResult(new {message = "invalid model"});
            }

            switch (model.GrantType)
            {
                case "password": return await GenerateNewToken(model);
                case "refresh_token": return await RefreshToken(model);
                default: return Unauthorized();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestModel model)
        {
            try
            {
                var refreshToken = _hslapiContext.Tokens.FirstOrDefault(x =>
                    x.ClientId == _jwtIssuerOptions.ClientId && x.Token == model.RefreshToken);

                if (refreshToken == null)
                    return new BadRequestObjectResult( new {message = "invalid refresh token"});
                if (refreshToken.ExpiresUtc < DateTime.UtcNow)
                    return new BadRequestObjectResult( new {message = "refresh token expired"});

                var user = await _userManager.FindByIdAsync(refreshToken.UserId);
                
                if (user == null)
                    return new BadRequestObjectResult( new {message = "user not found"});

                var newRefreshToken = CreateRefreshToken(refreshToken.ClientId, user.Id);
                _hslapiContext.Tokens.Remove(refreshToken);
                _hslapiContext.Tokens.Add(newRefreshToken);
                await _hslapiContext.SaveChangesAsync();

                var response = await CreateAccessToken(user, newRefreshToken.Token);
                return new OkObjectResult(new {access_token = response});
                
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new {message = e.Message, innerMessage = e.InnerException?.Message});
            }

            return new BadRequestResult();
        }

        private async Task<IActionResult> GenerateNewToken(TokenRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var newRefreshToken =
                    CreateRefreshToken(_jwtIssuerOptions.ClientId, user.Id); // client id might be null
                var oldRefreshTokens = _hslapiContext.Tokens.Where(x => x.UserId == user.Id);
                if (oldRefreshTokens != null)
                {
                    _hslapiContext.Tokens.RemoveRange(oldRefreshTokens);
                }

                _hslapiContext.Tokens.Add(newRefreshToken);
                await _hslapiContext.SaveChangesAsync();

                var accessToken = await CreateAccessToken(user, newRefreshToken.Token);
                return new OkObjectResult(new {access_token = accessToken});
            }

            return new UnauthorizedResult();
        }

        private async Task<TokenResponseModel> CreateAccessToken(User user, string rToken)
        {
            double tokenExpiryTime = Convert.ToDouble(_jwtIssuerOptions.Expiration);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppConfig.JwtSecret()));
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
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
                access_token = encoded_token,
                expires = access_token.ValidTo,
                refresh_token = rToken,
                roles = roles.FirstOrDefault(),
                username = user.UserName,
            };
        }

        private RefreshTokenModel CreateRefreshToken(string clientId, string userId)
        {
            return new RefreshTokenModel()
            {
                UserId = userId,
                ClientId = clientId,
                Token = Guid.NewGuid().ToString("N"),
                CreatedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(240), // more time
            };
        }
    }
}