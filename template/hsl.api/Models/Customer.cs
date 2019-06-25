using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hsl.api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public User Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
