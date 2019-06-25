using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hsl.dto.Models
{
    public class Customer : AppUser
    {
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
