using System;

namespace hsl.api.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
 
        public string RefreshToken { get; set; }
 
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
 
        public string TenantCode { get; set; }
 
        public DateTime TokenExpiration { get; set; }
    }
}