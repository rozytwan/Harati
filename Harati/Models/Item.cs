//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Harati.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public Nullable<int> Category { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    
        public virtual Category Category1 { get; set; }

        public List<Item> itemList { get; set; }
    }
}
