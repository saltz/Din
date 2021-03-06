﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Content;
using Din.Service.DTO.Content;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Search movies with a specific query.
        /// </summary>
        /// <param name="query">The movie title or a part of it.</param>
        /// <returns>ViewModel containing collections of existing movies and query results.</returns>
        Task<SearchResultDto<int, SearchMovie>> SearchMovieAsync(string query);

        /// <summary>
        /// Adds movie to the system.
        /// </summary>
        /// <param name="movie">The movie object that needs to be added.</param>
        /// <param name="id">The account id of the current session.</param>
        /// <returns>The status result.</returns>
        Task<ResultDto> AddMovieAsync(SearchMovie movie, int id);
        /// <summary>
        /// Get the MediaSystem movie release calendar.
        /// </summary>
        /// <returns>ViewModel containing calendar data.</returns>
        Task<IEnumerable<CalendarItemDto>> GetMovieCalendarAsync();
    }
}
