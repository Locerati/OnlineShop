using System;
using IceApp.Domain.Interfaces;
using IceApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IceApp.Tests
{
    public class CommentsControllerTests
    {
        [Fact]
        public void IndexReturnsComments()
        {
            var mock = new Mock<ICommentRepository>();
            mock.Setup(repo => repo.GetCountByProductId(1));

           

        }
    }
}
