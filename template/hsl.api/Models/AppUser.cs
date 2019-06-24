using Microsoft.AspNetCore.Identity;

namespace hsl.api.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}