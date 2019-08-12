using System;

namespace hsl.api.Models
{
    public class TokenResponseModel
    {
        public string access_token { get; set; }
        public DateTime expires { get; set; }
        public string refresh_token { get; set; }
        public string roles { get; set; }
        public string password { get; set; }
    }
}