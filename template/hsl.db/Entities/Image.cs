using System.Collections.Generic;
using hsl.api.Models;

namespace hsl.db.Entities
{
    public class Image
    {
        public int Id { set; get; } 
        public string Name { set; get; } 
        public string Caption { set; get; }
        
        public virtual IList<User> User { set; get; }
        public virtual IList<Product> Product { set; get; }
    }
}