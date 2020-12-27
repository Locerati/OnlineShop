using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Models;

namespace IceApp.Domain.ChildModels
{
    public class BasketInfo:Product
    {
        public int BasketId { get; set; }
        public int Quantity { get; set; }
    }
}
