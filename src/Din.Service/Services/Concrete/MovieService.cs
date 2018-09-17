using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Din.Service.DTO;
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

        public MovieService(DinContext context, IMovieClient movieClient, ITMDBClientConfig config) : base (context)
        {
            _movieClient = movieClient;
            _tmdbKey = config.Key;
        }

        public async Task<MovieDTO> SearchMovieAsync(string query)
        {
            return new MovieDTO
            {
                CurrentMovieCollection = await _movieClient.GetCurrentMoviesAsync(),
                QueryCollection = (await new TMDbClient(_tmdbKey).SearchMovieAsync(query)).Results
            };
        }

        public async Task<ResultDTO> AddMovieAsync(SearchMovie movie, int id)
        {
            var movieDate = Convert.ToDateTime(movie.ReleaseDate);
            var requestObj = new MCRequest
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
                MovieOptions = new MCRequestOptions
                {
                    SearchForMovie = true
                }
            };

            if (await _movieClient.AddMovieAsync(requestObj))
            {
                await LogContentAdditionAsync(movie.Title, id);
                return new ResultDTO
                {
                    Title = "Movie Added Successfully",
                    TitleColor = "#00d77c",
                    Message = "The Movie has been added 🤩\nYou can track the progress under your account content tab."
                };
            }
            else
            {
                return new ResultDTO
                {
                    Title = "Failed At adding Movie",
                    TitleColor = "#b43232",
                    Message = "Something went wrong 😵 Try again later!"
                };
            }
        }

        public async Task<IEnumerable<MCCalendarResponse>> GetMovieCalendarAsync()
        {
           return await _movieClient.GetCalendarAsync();
        }
    }
}
