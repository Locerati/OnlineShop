using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Application.Interfaces;
using IceApp.Domain.ChildModels;
namespace IceApp.Application.Services
{
    public class CommentService : ICommentService
    {
        public ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void Add(Comment comment)
        {
            _commentRepository.Add(comment);
        }
        public async Task<int> GetCountByProductId(int id)
        {
            return await _commentRepository.GetCountByProductId(id);
        }
        public IEnumerable<CommentsWithInfo> GetComments(int productid, int startnumber)
        {
            return _commentRepository.GetComments( productid, startnumber);
        }
    }
}
