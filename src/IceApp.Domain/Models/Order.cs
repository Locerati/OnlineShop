using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
