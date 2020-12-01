using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IceApp.Application.Interfaces;
using IceApp.Web.Models;
using AutoMapper;
using IceApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentService _commentService;
        private readonly IMapper _mapper;
        IWebHostEnvironment _appEnvironment;
        public CommentsController(ICommentService commentService, IMapper mapper,IWebHostEnvironment appEnviroment)
        {
            _commentService = commentService;
            _mapper = mapper;
            _appEnvironment = appEnviroment;
        }
        [HttpPost]
        public void AddComment(CommentViewModel commentViewModel)
        {
            if (commentViewModel.TextComment != null)
            {
                _commentService.Add(_mapper.Map<Comment>(commentViewModel));
            }

        }
        public async Task<int> GetCountComments(int productid)
        {
            int a = await _commentService.GetCountByProductId(productid);
            return a;
        }
        public JsonResult GetComments(int productid,int startnumber)
        {
            
          var Comments = _mapper.Map<IEnumerable<CommentViewModel>>(_commentService.GetComments(productid, startnumber));
            string path = "/images/badges/ann.jpg";
            foreach (CommentViewModel i in Comments)
            {
                if (i.Image.Length==0)
                {
                 
                    i.Image = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + path);

                }
                if(i.PersonName==null)
                {
                    i.PersonName = "Гость";
                }
            }
            return Json(Comments);
        }
    }
}
