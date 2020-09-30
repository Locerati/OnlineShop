using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
