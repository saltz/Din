﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Config.Interfaces;
using Din.Service.Dto;
using Din.Service.Dto.Content;
using Din.Service.DTO.Content;
using Din.Service.Services.Abstractions;
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

        public TvShowService(DinContext context, ITvShowClient tvShowClient, ITMDBClientConfig config, IMapper mapper) : base(context, mapper)
        {
            _tvShowClient = tvShowClient;
            _tmdbKey = config.Key;
        }

        public async Task<SearchResultDto<string, SearchTv>> SearchTvShowAsync(string query)
        {
            return new SearchResultDto<string, SearchTv>
            {
                CurrentCollection = (await _tvShowClient.GetCurrentTvShowsAsync()).Select(t => t.Title.ToLower()),
                QueryCollection = (await new TMDbClient(_tmdbKey).SearchTvShowAsync(query)).Results
            };
        }

        public async Task<ResultDto> AddTvShowAsync(SearchTv tvShow, int id)
        {
            var tmdbClient = new TMDbClient(_tmdbKey);
            var showDate = Convert.ToDateTime(tvShow.FirstAirDate);

            var seasons = new List<TcRequestSeason>();
            foreach (var s in (await tmdbClient.GetTvShowAsync(tvShow.Id)).Seasons)
                seasons.Add(new TcRequestSeason {SeasonNumber = s.SeasonNumber.ToString(), Monitored = true});

            var requestObj = new TcRequest
            {
                TvShowId = (await tmdbClient.GetTvShowExternalIdsAsync(tvShow.Id)).TvdbId,
                Title = tvShow.Name,
                QualityProfileId = 0,
                ProfileId = "6",
                TitleSlug = GenerateTitleSlug(tvShow.Name, showDate),
                Monitored = true,
                Seasons = seasons
            };

            var response = await _tvShowClient.AddTvShowAsync(requestObj);

            if (response.status)
            {
                await LogContentAdditionAsync(tvShow.Name, id, ContentType.TvShow, Convert.ToInt32(requestObj.TvShowId), response.systemId);

                return GenerateResultDto("Tv Show Added Successfully",
                    "The Movie has been added 🤩\nYou can track the progress under your account content tab.",
                    ResultDtoStatus.Successful);
            }
            else
            {
                return GenerateResultDto("Failed At adding Tv Show",
                    "Something went wrong 😵\nTry again later!",
                    ResultDtoStatus.Unsuccessful);
            }
        }

        public async Task<IEnumerable<CalendarItemDto>> GetTvShowCalendarAsync()
        {
            return Mapper.Map<IEnumerable<CalendarItemDto>>(await _tvShowClient.GetCalendarAsync());
        }
    }
}