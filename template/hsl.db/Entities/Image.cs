namespace hsl.api.Models
{
    public class Image
    {
        public int Id { set; get; } 
        public string Name { set; get; } 
        public string Caption { set; get; }
        public string UserOwnerId { set; get; }
        public string ProductOwnerId { set; get; }
        
        public virtual User UserOwner { set; get; }
        public virtual Product ProductOwner { set; get; }
        
    }
}