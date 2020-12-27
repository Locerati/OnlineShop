using IceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.ChildModels
{
    public class OrderInfo:Product
    {
        public int Quantity { get; set; }
    }
}
