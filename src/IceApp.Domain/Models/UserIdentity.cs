using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class UserIdentity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}
