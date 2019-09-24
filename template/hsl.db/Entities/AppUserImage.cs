using System.ComponentModel.DataAnnotations.Schema;

namespace hsl.db.Entities
{
    public class AppUserImage
    {
        public int Id { set; get; } 
        public string Name { set; get; } 

        [ForeignKey("AppUser")] public string AppUserId { set; get; }
        public virtual AppUser AppUser { set; get; }
    }
}