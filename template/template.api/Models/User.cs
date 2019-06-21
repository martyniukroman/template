namespace template.api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        
        public int PhoneId { get; set; }
        public Phone UserPhone { get; set; }
    }
}