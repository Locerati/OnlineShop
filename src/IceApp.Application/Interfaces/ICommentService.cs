using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;
namespace IceApp.Application.Interfaces
{
    public interface ICommentService
    {
        Task<int> GetCountByProductId(int id);
        void Add(Comment comment);
        public IEnumerable<CommentsWithInfo> GetComments(int productid, int startnumber);
    }
}
