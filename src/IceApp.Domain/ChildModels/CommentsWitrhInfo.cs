using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Models;
namespace IceApp.Domain.ChildModels
{
    public class CommentsWithInfo:Comment
    {
        public byte[] Image { get; set; }
        public string PersonName { get; set; }
    }
}
