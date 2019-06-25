using Microsoft.AspNetCore.Identity;

namespace hsl.api.Models
{
    public class User : IdentityUser<int>
    {
        public string UserName { set; get; }
    }
}