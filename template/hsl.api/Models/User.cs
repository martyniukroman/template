using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace hsl.api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public IList<TokenModel> Tokens { get; set; }
    }
}
