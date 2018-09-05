using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Controllers;
using Din.ExternalModels.ViewModels;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TMDbLib.Objects.Search;
using Xunit;

namespace Din.Tests.Controllers
{
    public class ContentControllerTest
    {
        [Fact]
        public void SearchMovieTest()
        {
            const string query = "thor";

            var movieResultViewModel = new MovieResultsViewModel
            {
                CurrentIdList = new List<int>
                {
                    1
                },
                QueryResult = new List<SearchMovie>
                {
                    new SearchMovie
                    {
                        Title = "Thor",
                        Id = 1
                    }
                }
            };

            var mockService = new Mock<IContentService>();
            mockService.Setup(_ => _.SearchMovieAsync(query)).Returns(Task.FromResult(movieResultViewModel));

            var controller = new ContentController(mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = controller.SearchMovieAsync(query);

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            var model = Assert.IsType<MovieResultsViewModel>(viewResult.Model);
            Assert.Equal("Thor", (model.QueryResult as List<SearchMovie>)?[0].Title);
            Assert.True(model.CurrentIdList.Count > 0);
        }

        [Fact]
        public void SearchMovieWithEmptyQueryTest()
        {
            const string query = null;
            var mockService = new Mock<IContentService>();
            var controller = new ContentController(mockService.Object);

            var result = controller.SearchMovieAsync(query);

            var actionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Main", actionResult.ControllerName);
        }

        [Fact]
        public void SearchTvShowTest()
        {
            const string query = "detective";

            var tvShowResultsViewModel = new TvShowResultsViewModel
            {
                CurrentTitleList = new List<string>
                {
                    "detective"
                },
                QueryResult = new List<SearchTv>
                {
                    new SearchTv
                    {
                        Name = "True Detective"
                    }
                }
            };

            var mockService = new Mock<IContentService>();
            mockService.Setup(_ => _.SearchTvShowAsync(query)).Returns(Task.FromResult(tvShowResultsViewModel));

            var controller = new ContentController(mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = controller.SearchTvShowAsync(query);

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            var model = Assert.IsType<TvShowResultsViewModel>(viewResult.Model);
            Assert.True(model.CurrentTitleList.Count > 0);
            Assert.True(model.QueryResult.Count > 0);
        }

        [Fact]
        public void SearchTvShowWithEmptyQueryTest()
        {
            const string query = null;
            var mockService = new Mock<IContentService>();
            var controller = new ContentController(mockService.Object);

            var result = controller.SearchTvShowAsync(query);

            var actionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Main", actionResult.ControllerName);
        }
    }
}