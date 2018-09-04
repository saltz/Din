using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.ExternalModels.ViewModels;
using Din.Service.Interfaces;
using Din.Service.Systems;
using Microsoft.EntityFrameworkCore;
using TMDbLib.Objects.Search;

namespace Din.Service.Concrete
{
    /// <inheritdoc />
    public class ContentService : IContentService
    {
        private readonly DinContext _context;

        public ContentService(DinContext context)
        {
            _context = context;
        } 

        public async Task<MovieResultsViewModel> SearchMovieAsync(string query)
        {
            var results = new MovieResultsViewModel
            {
                QueryResult = await new TmdbSystem().SearchMovieAsync(query),
                CurrentIdList = await new MediaSystem().GetCurrentMoviesAsync()
            };
            return results;
        }

        public async Task<TvShowResultsViewModel> SearchTvShowAsync(string query)
        {
            var results = new TvShowResultsViewModel()
            {
                QueryResult = await new TmdbSystem().SearchTvShowAsync(query),
                CurrentTtileList = await new MediaSystem().GetCurrentTvShowsAsync()
            };
            return results;
        }

        public async Task<ResultViewModel> AddMovieAsync(SearchMovie movie, int id)
        { 
            if (!(await new MediaSystem().AddMovieAsync(movie)).Equals(201))
            {
                return new ResultViewModel
                {
                    Title = "Failed At adding Movie",
                    TitleColor = "#b43232",
                    Message = "Something went wrong 😵   Try again later!"
                };
            }
            await LogContentAsync(movie.Title, await _context.Account.FirstAsync(a => a.ID.Equals(id)));
            return new ResultViewModel
            {
                Title = "Movie Added Successfully",
                TitleColor = "#00d77c",
                Message = "The Movie has been added 🤩   You can track the progress under your account profile tab."
            };
        }

        public async Task<ResultViewModel> AddTvShowAsync(SearchTv tvShow, int id)
        {
            var tmdbSystem = new TmdbSystem();
            if (!(await new MediaSystem().AddTvShowAsync(tvShow, await tmdbSystem.GetTvShowTvdbId(tvShow.Id), await tmdbSystem.GetTvShowSeasons(tvShow.Id))).Equals(201))
            {
                return new ResultViewModel
                {
                    Title = "Failed At adding Tv Show",
                    TitleColor = "#b43232",
                    Message = "Something went wrong 😵   Try again later!"
                };
            }
            await LogContentAsync(tvShow.Name, await _context.Account.FirstAsync(a => a.ID.Equals(id)));
            return new ResultViewModel
            {
                Title = "Tv Show Added Successfully",
                TitleColor = "#00d77c",
                Message = "The Movie has been added 🤩   You can track the progress under your account profile tab."
            };
        }

        private async Task LogContentAsync(string title, Account a)
        {
            if(a.AddedContent == null)
                a.AddedContent = new List<AddedContent>();
            _context.Attach(a);
            a.AddedContent.Add(new AddedContent(title, DateTime.Now, ContentStatus.Downloading, a));
            await _context.SaveChangesAsync();
        }


        //private async Task<List<AddedContent>> PerformUpdateAsync(Account account)
        //{
        //    var content = await _context.AddedContent.Where(ac => ac.Account.Equals(account) && !ac.Status.Equals(ContentStatus.Downloaded)).ToListAsync();
        //    _mediaSystem = new MediaSystem.MediaSystem(_propertyFile.get("mediaSystem"));
        //    _downloadSystem = new DownloadSystem.DownloadSystem(_propertyFile.get("downloadSystemUrl"), _propertyFile.get("downloadSystemPwd"));
        //    content = await _mediaSystem.CheckIfItemIsCompletedAsync(content);
        //    var downloadClientItems = await _downloadSystem.GetAllItemsAsync();
        //    foreach (var item in content)
        //    foreach (var dItem in downloadClientItems)
        //    {
        //        var titles = FixNames(item.Title, dItem.Name);
        //        if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
        //        var responseItem = await _downloadSystem.GetItemStatusAsync(dItem.Hash);
        //        item.Eta = responseItem.Eta;
        //        item.Percentage = (responseItem.FileProgress.Sum()) / responseItem.Files.Count;
        //        break;
        //    }

        //    _context.Attach(account);
        //    account.AddedContent.AddRange(content);
        //    await _context.SaveChangesAsync();
        //    return account.AddedContent;
        //}

        //private string[] FixNames(string title1, string title2)
        //{
        //    title1 = title1.Replace(" ", ".");
        //    if (title2.Contains("1080p"))
        //        title2 = title2.Substring(0, title2.IndexOf("1080p", StringComparison.Ordinal));

        //    if (title2.Contains("720p"))
        //        title2 = title2.Substring(0, title2.IndexOf("720p", StringComparison.Ordinal));

        //    title1 = Regex.Replace(title1, @"[\d-]", string.Empty);
        //    title2 = Regex.Replace(title2, @"[\d-]", string.Empty);
        //    return new[] { title1, title2 };
        //}
    }
}