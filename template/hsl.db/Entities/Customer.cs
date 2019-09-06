namespace hsl.db.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string IdentityId { get; set; }
        public AppUser IdentityAppUser { get; set; }  // navigation property
    }
}
