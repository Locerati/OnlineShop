using IceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceApp.Web.Models
{
    public class SubcategoriesListViewModel
    {
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SubcategoryViewModel> subcategories { get; set; }
    }
}
