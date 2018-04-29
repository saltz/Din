using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Content;
using Din.ExternalModels.DownloadClient;
using Din.ExternalModels.Entities;
using Din.Logic.TMDB;
using Microsoft.EntityFrameworkCore;
using TMDbLib.Objects.Search;

namespace Din.Logic
{
    public class ContentManager
    {
        private TmdbSystem _tmdbSystem;
        private MediaSystem.MediaSystem _mediaSystem;
        private DownloadSystem.DownloadSystem _downloadSystem;
        private readonly DinContext _context;
        private PropertyFile _propertyFile;

        public ContentManager()
        {
            InitilizeProperties();
        }

        public ContentManager(DinContext context)
        {
            InitilizeProperties();
            _context = context;
        }
    
        public async Task<string> GenerateBackgroundAsync()
        {
            var httpRequest = new HttpRequestHelper(_propertyFile.get("unsplash"), false);
            return await httpRequest.PerformGetRequestAsync();
        }


        public async Task<string> GetRandomGifAsync(GiphyQuery query)
        {
            HttpRequestHelper httpRequest;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyPageNotFound"), false);
                    break;
                case GiphyQuery.Forbidden:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyForbidden"), false);
                    break;
                case GiphyQuery.Logout:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyLogout"), false);
                    break;
                case GiphyQuery.ServerError:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyServerError"), false);
                    break;
                case GiphyQuery.Random:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyRandom"), false);
                    break;
                default:
                    httpRequest = new HttpRequestHelper(_propertyFile.get("giphyRandom"), false);
                    break;
            }
            var response = await httpRequest.PerformGetRequestAsync();
            return response;
        }

        public async Task<List<SearchMovie>> TmdbSearchMovieAsync(string searchQuery)
        {
            _tmdbSystem = new TmdbSystem(_propertyFile.get("tmdb"));
            return await _tmdbSystem.SearchMovie(searchQuery);
        }

        public async Task<List<int>> MediaSystemGetCurrentMoviesAsync()
        {
            _mediaSystem = new MediaSystem.MediaSystem(_propertyFile.get("mediaSystem"));
            return await _mediaSystem.GetCurrentMoviesAsync();
        }

        public async Task<bool> MediaSystemAddMovie(SearchMovie movie, Account account)
        {
            _mediaSystem = new MediaSystem.MediaSystem(_propertyFile.get("mediaSystem"));
            var responseCode = await _mediaSystem.AddMovieAsync(movie);
            if (!responseCode.Equals(201)) return false;
            await LogContentAsync(movie, account); 
            return true;
        }

        public async Task<List<AddedContent>> GetAddedContent(Account a)
        {
            return await PerformUpdateAsync(a);
        }

        private async Task LogContentAsync(SearchMovie m, Account a)
        {
            if(a.AddedContent == null)
                a.AddedContent = new List<AddedContent>();
            _context.Attach(a);
            a.AddedContent.Add(new AddedContent(m.Title, DateTime.Now, ContentStatus.Added, a));
            await _context.SaveChangesAsync();
        }

        private void InitilizeProperties()
        {
            try
            {
                _propertyFile = new PropertyFile(Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.UserProfile), "Din" + Path.DirectorySeparatorChar + "properties"));
            }
            catch (IOException)
            {
                Console.WriteLine("Property file not found");
            }
        }

        private async Task<List<AddedContent>> PerformUpdateAsync(Account account)
        {
            var content = await _context.AddedContent.Where(ac => ac.Account.Equals(account) && !ac.Status.Equals(ContentStatus.Downloaded)).ToListAsync();
            _mediaSystem = new MediaSystem.MediaSystem(_propertyFile.get("mediaSystem"));
            _downloadSystem = new DownloadSystem.DownloadSystem(_propertyFile.get("downloadSystemUrl"), _propertyFile.get("downloadSystemPwd"));
            content = await _mediaSystem.CheckIfItemIsCompletedAsync(content);
            var downloadClientItems = await _downloadSystem.GetAllItemsAsync();
            foreach (var item in content)
            foreach (var dItem in downloadClientItems)
            {
                var titles = FixNames(item.Title, dItem.Name);
                if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
                var responseItem = await _downloadSystem.GetItemStatusAsync(dItem.Hash);
                item.Eta = responseItem.Eta;
                item.Percentage = (responseItem.FileProgress.Sum()) / responseItem.Files.Count;
                break;
            }

            _context.Attach(account);
            account.AddedContent.AddRange(content);
            await _context.SaveChangesAsync();
            return account.AddedContent;
        }

        private string[] FixNames(string title1, string title2)
        {
            title1 = title1.Replace(" ", ".");
            if (title2.Contains("1080p"))
                title2 = title2.Substring(0, title2.IndexOf("1080p", StringComparison.Ordinal));

            if (title2.Contains("720p"))
                title2 = title2.Substring(0, title2.IndexOf("720p", StringComparison.Ordinal));

            title1 = Regex.Replace(title1, @"[\d-]", string.Empty);
            title2 = Regex.Replace(title2, @"[\d-]", string.Empty);
            return new[] { title1, title2 };
        }
    }
}