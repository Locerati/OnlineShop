using IceApp.Domain.ChildModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceApp.Web.Models
{
    public class BasketViewModel
    {
        public IEnumerable<BasketInfo> BasketsItems { get; set; }
        public int CountOfProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PriceWithSale { get; set; }
    }
}
