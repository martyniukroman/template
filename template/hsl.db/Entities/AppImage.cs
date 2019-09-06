using System.Collections.Generic;
using hsl.api.Models;

namespace hsl.db.Entities
{
    public class AppImage
    {
        public int Id { set; get; } 
        public string Name { set; get; } 
        public string Caption { set; get; }

        public int? AppUserId { set; get; }
        public int? ProductId { set; get; }
        
        public virtual AppUser AppUser { set; get; }
        public virtual Product Product { set; get; }
    }
}