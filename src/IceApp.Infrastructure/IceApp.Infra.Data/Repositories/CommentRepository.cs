using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using IceApp.Domain.ChildModels;
namespace IceApp.Infra.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private string _connect;
        public CommentRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }
        public void Add(Comment comment)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute("INSERT INTO Comments (ProductId,UserId,TextComment) Values (@ProductId,@UserId,@TextComment);", comment);
            }
        }
        public async Task<int> GetCountByProductId(int id)
        {
            int count;
            using (var db = new NpgsqlConnection(_connect))
            {
                count = await db.QueryFirstOrDefaultAsync<int>($"select count(*) as count from comments group by productid having productid = {id};");
            }
            return (count);
        }
        public IEnumerable<CommentsWithInfo> GetComments(int productid, int startnumber)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return db.Query<CommentsWithInfo>("SELECT Comments.id as Id,comments.textcomment as textcomment,users.image as Image,users.username as PersonName FROM comments"
                                                    + " LEFT JOIN Products ON Products.Id = comments.ProductId"
                                                    + $" LEFT JOIN users ON users.Id = comments.userid where productid = {productid} LIMIT 3 OFFSET {startnumber}; ");
            }
        }
    }
}
