using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace IceApp.Web.Models
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 40 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указано описание товара")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Не указан вес товара")]
        [Range(typeof(int), "0", "2147000000", ErrorMessage = "Наименьшая цена - 0 рублей")]
        public int Weight { get; set; }
        public byte[] Image { get; set; }
        public long DeliveryPeriod { get; set; }
        [Required(ErrorMessage = "Не указана цена")]
        [Range(typeof(decimal), "0,0", "1000000,6", ErrorMessage = "Наименьшая цена - 0 рублей")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int? CountOfComments { get; set; }
    }
}
