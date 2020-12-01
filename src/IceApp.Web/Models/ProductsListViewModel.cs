using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IceApp.Web.Models
{
    public class ProductsListViewModel
    {
        public int? CategoryId { get; set; }
        public string ParentName { get; set; }
        public IEnumerable<ProductViewModel> products { get; set; }
 
    }
}
