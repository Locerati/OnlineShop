using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Models;

namespace IceApp.Domain.ChildModels
{
    public class ProductCountComments:Product
    {
       public int CountOfComments { get; set; }
    }
}
