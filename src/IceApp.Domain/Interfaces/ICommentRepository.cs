using IceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.ChildModels;
namespace IceApp.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> GetCountByProductId(int id);
        void Add(Comment comment);
        public IEnumerable<CommentsWithInfo> GetComments(int productid, int startnumber);
    }
}
