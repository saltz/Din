using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Content;
using Din.Service.DTO.Content;
using Din.Service.Services.Interfaces;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IMovieService" />
    public class MovieService : ContentService, IMovieService
    {
        private readonly IMovieClient _movieClient;
        private readonly string _tmdbKey;

        public MovieService(DinContext context, IMovieClient movieClient, ITMDBClientConfig config, IMapper mapper) : base (context, mapper)
        {
            _movieClient = movieClient;
            _tmdbKey = config.Key;
        }

        public async Task<MovieDto> SearchMovieAsync(string query)
        {
            return new MovieDto
            {
                CurrentMovieCollection = await _movieClient.GetCurrentMoviesAsync(),
                QueryCollection = (await new TMDbClient(_tmdbKey).SearchMovieAsync(query)).Results
            };
        }

        public async Task<ResultDto> AddMovieAsync(SearchMovie movie, int id)
        {
            var movieDate = Convert.ToDateTime(movie.ReleaseDate);
            var requestObj = new McRequest
            {
                Title = movie.Title,
                Year = movieDate.Year,
                QualityProfileId = 0,
                ProfileId = "6",
                TitleSlug = GenerateTitleSlug(movie.Title, movieDate),
                Monitored = true,
                TmdbId = movie.Id,
                Images = new List<ContentRequestObjectImage>
                {
                    new ContentRequestObjectImage
                    {
                        CoverType = "poster",
                        Url = movie.PosterPath
                    }
                },
                MovieOptions = new McRequestOptions
                {
                    SearchForMovie = true
                }
            };

            if (await _movieClient.AddMovieAsync(requestObj))
            {
                await LogContentAdditionAsync(movie.Title, id);
                return new ResultDto
                {
                    Title = "Movie Added Successfully",
                    TitleColor = "#00d77c",
                    Message = "The Movie has been added 🤩\nYou can track the progress under your account content tab."
                };
            }
            else
            {
                return new ResultDto
                {
                    Title = "Failed At adding Movie",
                    TitleColor = "#b43232",
                    Message = "Something went wrong 😵 Try again later!"
                };
            }
        }

        public async Task<IEnumerable<CalendarItemDto>> GetMovieCalendarAsync()
        {
            return Mapper.Map<IEnumerable<CalendarItemDto>>(await _movieClient.GetCalendarAsync());
        }
    }
}
