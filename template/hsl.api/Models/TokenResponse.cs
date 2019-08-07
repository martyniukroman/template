using System;

namespace hsl.api.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationUtc { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
