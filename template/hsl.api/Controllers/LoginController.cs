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
//    public class RToken
//    {
//        public string RefreshToken { get; set; }
//    }

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
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel creds)
        {
            return new NotFoundResult();
        }
    }
}