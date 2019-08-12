using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using hsl.api.Interfaces;
using hsl.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace hsl.api.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtIssuerOptions _appSettings;
        private readonly hslapiContext _context;
        private readonly UserManager<User> _userManager;
        private const string SecretKey = "bed77aaafefas5c57fc865fasf6c0a1e2533760";

        public TokenService(hslapiContext context, IOptions<JwtIssuerOptions> settings, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appSettings = settings.Value;
            _context = context;
        }

        public Task<JwtSecurityToken> GenerateAccessToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
