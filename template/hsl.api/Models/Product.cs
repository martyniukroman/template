using System;
using System.Buffers.Text;

namespace hsl.api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string PicUrl { get; set; }
        public string PicBase64 { set; get; }
        public string StockCount { set; get; }
        public double Price { set; get; }
        public DateTime PublishDate { get; set; }
    }
}