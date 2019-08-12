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
    public class TokenController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        private readonly RefreshTokenModel _refreshTokenModel;
        private readonly UserManager<User> _userManager;
        private readonly hslapiContext _hslapiContext;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public TokenController(UserManager<User> userManager,
            ITokenService tokenService, RefreshTokenModel refreshTokenModel, hslapiContext hslapiContextMir, IOptions<JwtIssuerOptions> options)
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
            }
            
        }

        private async Task<IActionResult> RefreshToken(TokenRequestModel model)
        {
            throw new System.NotImplementedException();
        }

        private async Task<IActionResult> GenerateNewToken(TokenRequestModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}