using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public List<Product> Products { get; set; }
    }
}
