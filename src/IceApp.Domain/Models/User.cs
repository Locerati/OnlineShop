using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Comment> Comments { get; set; }

        //public string Password{get;set;}
    }
}
