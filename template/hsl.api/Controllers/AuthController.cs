using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Helpers;
using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace hsl.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this._userManager = userManager;
            this._jwtFactory = jwtFactory;
            this._jwtOptions = jwtOptions.Value;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel creds)
        {
            var identity = await GetClaimsIdentity(creds.UserName, creds.Password);
            if (identity == null)
            {
                return BadRequest(new {message = "Invalid credentials"});
                // return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var jwt = await TokensHelper.GenerateJwt(identity, _jwtFactory, creds.UserName, _jwtOptions,
                new JsonSerializerSettings {Formatting = Formatting.Indented});
            
            return new OkObjectResult(jwt);
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