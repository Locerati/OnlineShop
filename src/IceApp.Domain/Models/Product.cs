using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public byte[] Image { get; set; }
        public long DeliveryPeriod { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Categorie { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
