namespace hsl.dto.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Customer Buyer { set; get; }
    }
}