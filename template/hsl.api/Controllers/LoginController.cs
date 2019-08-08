using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hsl.api.Controllers
{
    public class RToken
    {
        public string RefreshToken { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<User> _userManager;

        public LoginController(UserManager<User> userManager,
            ITokenService tokenService,
            IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] CredentialsViewModel creds)
        {
            var identity = await GetClaimsIdentity(creds.UserName, creds.Password);
            if (identity == null)
            {
                return BadRequest(new {message = "Invalid credentials"});
            }

            var user = await _userManager.FindByNameAsync(creds.UserName);
            IActionResult status = Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessToken(user)),
                refresh_token = _tokenService.GenerateRefreshToken(user).Token
            });

            return status;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RToken refreshToken)
        {
            var refreshedToken = _tokenService.RefreshTokenValidation(refreshToken.RefreshToken);
            if (refreshedToken == null)
            {
                return BadRequest(new {message = "invalid_grant"});
            }

            var user = await _userManager.FindByIdAsync(refreshedToken.UserId);
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessToken(user)),
                refresh_token = refreshedToken.Token
            });
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToCheck = await _userManager.FindByNameAsync(userName);
            if (userToCheck == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToCheck, password))
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToCheck.Id));

            return await Task.FromResult<ClaimsIdentity>(null);
        }



    }
}
