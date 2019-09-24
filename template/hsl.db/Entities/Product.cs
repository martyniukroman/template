using System;
using System.Collections.Generic;

namespace hsl.db.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string PictureUrl { get; set; }
        public int StockCount { set; get; }
        public int InCart { set; get; }
        public double Price { set; get; }
        public DateTime PublishDate { get; set; }
        
        public IList<AppProductImage> Images { get; set; }
    }
}