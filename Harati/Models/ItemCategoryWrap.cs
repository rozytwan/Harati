using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harati.Models
{
    public class ItemCategoryWrap
    {
       public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}