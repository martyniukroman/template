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
        private const string APPROVED = "Approved";
        private const string DOCTOR = "Doctor";
        private const int MAXIMUM_LOGGED_DEVICES = 5;
        private const int REFRESH_TOKEN_LIFETIME_DAYS = 15;
        private const string SecretKey = "bed77aaafefas5c57fc865fasf6c0a1e2533760";

        public TokenService(hslapiContext context, IOptions<JwtIssuerOptions> settings, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appSettings = settings.Value;
            _context = context;
        }

        /// <summary>
        /// Method for generation access tokens for app users
        /// </summary>
        /// <param name="user"></param>
        /// <returns>access token</returns>
        public async Task<JwtSecurityToken> GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _appSettings.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_appSettings.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("bed77aaafefas5c57fc865fasf6c0a1e2533760")); // ascii
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var currentTime = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _appSettings.Issuer,
                notBefore: currentTime,
                claims: claims,
                expires: currentTime.AddDays(1),
                signingCredentials: credential);
            return token;
        }

        /// <summary>
        /// Method for generation and saving in DB refresh tokens for renewing access tokens
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RefreshTokenModel GenerateRefreshToken(User user)
        {
            var userTokens = _context.Tokens.Where(x => x.UserId == user.Id);
            //you have to relogin if number of logged devices higher than maximum allowed
            if (userTokens.Count() > MAXIMUM_LOGGED_DEVICES)
            {
                foreach (var item in userTokens)
                {
                    _context.Tokens.Remove(item);
                }

                _context.SaveChanges();
            }

            var newRefreshToken = new RefreshTokenModel
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                ExpiresUtc = DateTime.UtcNow.AddDays(REFRESH_TOKEN_LIFETIME_DAYS),
                Revoked = false,
            };
            _context.Tokens.Add(newRefreshToken);
            _context.SaveChanges();
            return newRefreshToken;
        }

        /// <summary>
        /// Method for validation of refresh token that was sent by user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public RefreshTokenModel RefreshTokenValidation(string token)
        {
            RefreshTokenModel refreshedToken = _context.Tokens.FirstOrDefault(x => x.Token == token);

            if (refreshedToken == null)
                return null;

            if (refreshedToken.ExpiresUtc < DateTime.UtcNow || refreshedToken.Revoked == true)
            {
                _context.Tokens.Remove(refreshedToken);
                _context.SaveChanges();
                return null;
            }

            string newRefreshToken = Guid.NewGuid().ToString();
            refreshedToken.Token = newRefreshToken;
            _context.SaveChanges();
            return refreshedToken;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long) Math.Round((date.ToUniversalTime() -
                                  new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
