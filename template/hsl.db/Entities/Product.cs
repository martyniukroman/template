using System;
using System.Buffers.Text;

namespace hsl.api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string PictureUrl { get; set; }
        public string StockCount { set; get; }
        public double Price { set; get; }
        public DateTime PublishDate { get; set; }
        
        public virtual Image Picture { get; set; }
    }
}