using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
