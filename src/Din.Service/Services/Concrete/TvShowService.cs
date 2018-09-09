using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Config.Interfaces;
using Din.Service.DTO;
using Din.Service.DTO.Content;
using Din.Service.Services.Interfaces;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="ITvShowService" />
    public class TvShowService : ContentService, ITvShowService
    {
        private readonly ITvShowClient _tvShowClient;
        private readonly string _tmdbKey;

        public TvShowService(DinContext context, ITvShowClient tvShowClient, ITMDBClientConfig config) : base(context)
        {
            _tvShowClient = tvShowClient;
            _tmdbKey = config.Key;
        }

        public async Task<TvShowDTO> SearchTvShowAsync(string query)
        {
            return new TvShowDTO
            {
                CurrentTvShowCollection = await _tvShowClient.GetCurrentTvShowsAsync(),
                QueryCollection = (await new TMDbClient(_tmdbKey).SearchTvShowAsync(query)).Results
            };
        }

        public async Task<ResultDTO> AddTvShowAsync(SearchTv tvShow, int id)
        {
            var tmdbClient = new TMDbClient(_tmdbKey);
            var showDate = Convert.ToDateTime(tvShow.FirstAirDate);

            var seasons = new List<TCRequestSeason>();
            foreach (var s in (await tmdbClient.GetTvShowAsync(tvShow.Id)).Seasons)
                seasons.Add(new TCRequestSeason {SeasonNumber = s.SeasonNumber.ToString(), Monitored = true});

            var requestObj = new TCRequest
            {
                TvShowId = (await tmdbClient.GetTvShowExternalIdsAsync(tvShow.Id)).TvdbId,
                Title = tvShow.Name,
                QualityProfileId = 0,
                ProfileId = "6",
                TitleSlug = GenerateTitleSlug(tvShow.Name, showDate),
                Monitored = true,
                Seasons = seasons
            };

            if (await _tvShowClient.AddTvShowAsync(requestObj))
            {
                await LogContentAdditionAsync(tvShow.Name, id);
                return new ResultDTO
                {
                    Title = "Tv Show Added Successfully",
                    TitleColor = "#00d77c",
                    Message = "The Movie has been added 🤩<br>You can track the progress under your account profile tab."
                };
            }
            else
            {
                return new ResultDTO
                {
                    Title = "Failed At adding Tv Show",
                    TitleColor = "#b43232",
                    Message = "Something went wrong 😵   Try again later!"
                };
            }
        }
    }
}