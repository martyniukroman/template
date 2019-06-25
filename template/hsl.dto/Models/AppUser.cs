using Microsoft.AspNetCore.Identity;

namespace hsl.dto.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}