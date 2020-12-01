using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IceApp.Web.Models
{
    public class SubcategoryViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Не указано название подкатегории")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 40 символов")]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int ParentId { get; set; }
    }
}
