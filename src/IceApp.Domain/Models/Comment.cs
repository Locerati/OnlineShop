using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string TextComment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }


    }
}
