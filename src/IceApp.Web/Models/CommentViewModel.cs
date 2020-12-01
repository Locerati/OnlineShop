using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IceApp.Web.Models
{
    public class CommentViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string TextComment { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public byte[] Image { get; set; }
        public string PersonName { get; set; }
    }
}
