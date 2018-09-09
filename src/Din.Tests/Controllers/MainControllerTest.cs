using Din.Controllers;
using Din.Service.DTO;
using Din.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Din.Tests.Controllers
{
    public class MainControllerTest : IClassFixture<MainFixture>
    {
        private readonly MainFixture _fixture;

        public MainControllerTest(MainFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IndexUnAuthenticatedTest()
        {
            var controller = new MainController(_fixture.MockService.Object)
            {
                ControllerContext = _fixture.DefaultControllerContext()
            };

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void IndexAuthenticatedTest()
        {
            _fixture.MockService.Setup(service => service.GenerateBackgroundImages()).ReturnsAsync(new MediaDTO());

            var controller = new MainController(_fixture.MockService.Object)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.Equal("Home", viewResult.ViewName);
        }
    }
}