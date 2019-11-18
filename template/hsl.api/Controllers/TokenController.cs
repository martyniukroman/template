using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Interfaces;
using hsl.bl.Services;
using hsl.db.Entities;
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
        private readonly IToken _tokenService;

        public TokenController(
            IToken tokenService
        )
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] TokenRequestModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult(new ErrorViewModel
                {
                    code = 400,
                    caption = "User not found",
                    tag = "notFoundError"
                });
            }

            switch (model.GrantType)
            {
                case "password": return await _tokenService.GenerateNewToken(model);
                case "refresh_token": return await RefreshToken(model);
                default: return Unauthorized();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestModel model)
        {
            return await _tokenService.RefreshToken(model);
        }
    }
}