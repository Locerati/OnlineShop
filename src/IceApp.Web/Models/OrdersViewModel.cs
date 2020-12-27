using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceApp.Domain.Models;
namespace IceApp.Web.Controllers
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> OrderList { get; set; }

    }
}
