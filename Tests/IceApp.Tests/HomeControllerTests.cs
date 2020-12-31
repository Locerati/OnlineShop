using System;
using IceApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IceApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;

        }
        [Fact]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void IndexViewNameEqualIndex()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.Equal("Index",result?.ViewName);
        }
    }
}
