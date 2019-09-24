using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using hsl.api.Models;

namespace hsl.db.Entities
{
    public class AppProductImage
    {
        public int Id { set; get; } 
        public string Name { set; get; } 
        public string Caption { set; get; }

        public Product Product { set; get; }
    }
}