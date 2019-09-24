using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using hsl.api.Models;
using Microsoft.AspNetCore.Identity;

namespace hsl.db.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public int Type { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public IList<RefreshTokenModel> Tokens { get; set; }

        public virtual AppUserImage AppUserImage { get; set; }
    }
}