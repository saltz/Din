using System;
using System.Collections.Generic;
using Din.Controllers;
using Din.Service.Dto;
using Din.Service.Dto.Content;
using Din.Tests.Fixtures;
using Din.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;
using Xunit;

namespace Din.Tests.Controllers
{
    public class TvShowControllerTest : IClassFixture<TvShowFixture>
    {
        private readonly TvShowFixture _fixture;

        public TvShowControllerTest(TvShowFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SearchTvShowAsync()
        {
            const string query = "TvShowTitle";
            var tvShowDto = new TvShowDto
            {
                CurrentTvShowCollection = new List<string>
                {
                    "True Detective"
                },
                QueryCollection = new List<SearchTv>()
            };
            _fixture.MockService.Setup(_ => _.SearchTvShowAsync(query)).ReturnsAsync(tvShowDto);
            var controller = new TvShowController(_fixture.MockService.Object, _fixture.Mapper);

            var result = controller.SearchTvShowAsync(query);

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            Assert.IsType<TvShowResultsViewModel>(viewResult.Model);
        }

        [Fact]
        public void AddTvShowAsyncTest()
        {
            var resultDto = new ResultDto
            {
                Title = "Success"
            };
            _fixture.MockService.Setup(_ => _.AddTvShowAsync(It.IsAny<SearchTv>(), Convert.ToInt32(TestConsts.Id)))
                .ReturnsAsync(resultDto);

            var controller = new TvShowController(_fixture.MockService.Object, _fixture.Mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.AddTvShowAsync(JsonConvert.SerializeObject(new SearchTv()));

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            Assert.IsType<ResultViewModel>(viewResult.Model);
        }
    }
}
