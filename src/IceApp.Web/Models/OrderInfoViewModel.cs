using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;
namespace IceApp.Web.Models
{
    public class OrderInfoViewModel
    {
        public int Id { get;  set; }
        public string Address { get; set; }
        public string DeliveryMethod { get; set; }
        
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int CountOfProducts { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public IEnumerable<OrderInfo> ProductsList {get;set;}
    }
}
