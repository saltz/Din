using System;
using AutoMapper;
using Din.Controllers;
using Din.MapperProfiles;
using Din.Service.DTO.Account;
using Din.Tests.Fixtures;
using Din.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Din.Tests.Controllers
{
    public class AccountControllerTest : IClassFixture<AccountFixture>
    {
        private readonly AccountFixture _fixture;
        private readonly IMapper _mapper;

        public AccountControllerTest(AccountFixture fixture)
        {
            _fixture = fixture;
            _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile(new EntityProfile()); }));
        }

        [Fact]
        public void GetUserViewAsyncTest()
        {
            _fixture.AccountServiceMock.Setup(_ => _.GetAccountDataAsync(Convert.ToInt32(TestConsts.Id)))
                .ReturnsAsync(new DataDTO());
            var controller = new AccountController(_fixture.AccountServiceMock.Object, _fixture.MovieServiceMock.Object,
                _fixture.TvShowServiceMock.Object, _mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };
            controller.Request.Headers["User-Agent"] = TestConsts.UserAgent;

            var result = controller.GetUserViewAsync().Result;
            var viewResult = Assert.IsType<PartialViewResult>(result);
            Assert.IsType<AccountViewModel>(viewResult.Model);
        }
    }
}