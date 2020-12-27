﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Domain.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

    }
}
