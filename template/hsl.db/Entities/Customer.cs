namespace hsl.db.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string IdentityId { get; set; }
        public User IdentityUser { get; set; }  // navigation property
    }
}
