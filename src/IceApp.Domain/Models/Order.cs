using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
