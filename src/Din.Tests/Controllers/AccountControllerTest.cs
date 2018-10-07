using System;
using System.IO;
using AutoMapper;
using Din.Controllers;
using Din.Mapping.Profiles;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Context;
using Din.Tests.Fixtures;
using Din.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new ViewModelToDtoProfile());
            }));
        }

        [Fact]
        public void GetUserViewAsyncTest()
        {
            _fixture.ServiceMock.Setup(_ => _.GetAccountDataAsync(Convert.ToInt32(TestConsts.Id)))
                .ReturnsAsync(new DataDto());
            var controller = new AccountController(_fixture.ServiceMock.Object, _mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };
            controller.Request.Headers["User-Agent"] = TestConsts.UserAgent;

            var result = controller.GetUserViewAsync().Result;
            var viewResult = Assert.IsType<PartialViewResult>(result);
            Assert.IsType<AccountViewModel>(viewResult.Model);
        }

        [Fact]
        public void UploadAccountImageAsyncTest()
        {
            var imageData = new byte[] {0, 0, 0, 1, 0, 1};

            _fixture.ServiceMock
                .Setup(_ => _.UploadAccountImageAsync(Convert.ToInt32(TestConsts.Id), TestConsts.ImageName, imageData))
                .ReturnsAsync(new ResultDto());

            var controller = new AccountController(_fixture.ServiceMock.Object, _mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.UploadAccountImageAsync(new FormFile(Stream.Null, 0, 0, "image", "filename")).Result;
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void UpdatePersonalInformationAsyncTest()
        {
            var userDto = new UserDto
            {
                FirstName = TestConsts.Firstname,
                LastName = TestConsts.Lastname
            };

            _fixture.ServiceMock.Setup(_ => _.UpdatePersonalInformation(Convert.ToInt32(TestConsts.Id), userDto))
                .ReturnsAsync(new ResultDto());

            var controller = new AccountController(_fixture.ServiceMock.Object, _mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.UpdatePersonalInformation(TestConsts.Firstname, TestConsts.Lastname).Result;
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void UpdateAccountInformationAsyncTest()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(TestConsts.Password);

            _fixture.ServiceMock.Setup(_ =>
                    _.UpdateAccountInformation(Convert.ToInt32(TestConsts.Id), TestConsts.Username, hash))
                .ReturnsAsync(new ResultDto());

            var controller = new AccountController(_fixture.ServiceMock.Object, _mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.UpdateAccountInformation(TestConsts.Username, TestConsts.Password).Result;
            Assert.IsType<PartialViewResult>(result);
        }
    }
}