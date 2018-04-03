using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Din.ExternalModels.Content;
using Din.ExternalModels.DownloadClient;
using Din.Logic.TMDB;
using TMDbLib.Objects.Search;

namespace Din.Logic
{
    public class ContentManager
    {
        private TmdbSystem _tmdbSystem;
        private MediaSystem.MediaSystem _mediaSystem;
        private DownloadSystem.DownloadSystem _downloadSystem;

        public string GenerateBackground()
        {
            var httpRequest = new HttpRequestHelper(Properties.PrivateStrings.unsplash);
            return httpRequest.PerformGetRequest();
        }


        public string GetRandomGif(GiphyQuery query)
        {
            HttpRequestHelper httpRequest;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    httpRequest = new HttpRequestHelper(Properties.PrivateStrings.giphyPageNotFound);
                    break;
                case GiphyQuery.Forbidden:
                    httpRequest = new HttpRequestHelper(Properties.PrivateStrings.giphyForbidden);
                    break;
                case GiphyQuery.Logout:
                    httpRequest = new HttpRequestHelper(Properties.PrivateStrings.giphyLogout);
                    break;
                case GiphyQuery.ServerError:
                    httpRequest = new HttpRequestHelper(Properties.PrivateStrings.giphyServerError);
                    break;
                default:
                    httpRequest = new HttpRequestHelper(Properties.PrivateStrings.giphyRandom);
                    break;
            }
            return httpRequest.PerformGetRequest();
        }

        public List<SearchMovie> TmdbSearchMovie(string searchQuery)
        {
            _tmdbSystem = new TmdbSystem(Properties.PrivateStrings.tmdb);
            return _tmdbSystem.SearchMovie(searchQuery);
        }

        //TODO
        public bool MediaSystemAddMovie(SearchMovie movie, Object userAdObject)
        {
            _mediaSystem = new MediaSystem.MediaSystem(Properties.PrivateStrings.mediaSystem);
            var responseCode = _mediaSystem.AddMovie(movie);
            if (!responseCode.Equals(400))
            {
                // ReportEvent(userAdObject, movie.Title, "added"); SUCCESS
                return true;
            }
            //  ReportEvent(userAdObject, movie.Title, "failed to add"); FAILED
            return false;
        }

        public List<int> MediaSystemGetCurrentMovies()
        {
            _mediaSystem = new MediaSystem.MediaSystem(Properties.PrivateStrings.mediaSystem);
            return _mediaSystem.GetCurrentMovies();
        }

        //TODO
        //public static List<ContentStatusObject> GetContentStatus(Object user)
        //{
        //    UpdateContentStatus(user);
        //    var databaseContent = new DatabaseContent();
        //    return databaseContent.GetMoviesByAccountname(user.SAMAccountName);
        //}

       
        //TODO
        //private static void UpdateContentStatus(ADObject user)
        //{
        //    var databaseContent = new DatabaseContent();
        //    var movies = databaseContent.GetMoviesByAccountname(user.SAMAccountName);
        //    var itemsToCheck = movies.Where(cso => !cso.Status.Equals("downloaded")).ToList();
        //    itemsToCheck = CheckIfItemIsCompleted(itemsToCheck);
        //    if (!DownloadClient.Authenticate()) return;
        //    var downloadClientItems = DownloadClient.GetAllItems();
        //    foreach (var item in itemsToCheck)
        //        foreach (var dItem in downloadClientItems)
        //        {
        //            var titles = FixNames(item.Title, dItem.Name);
        //            if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
        //            var responseItem = DownloadClient.GetItemStatus(dItem.Hash);
        //            item.Eta = responseItem.Eta;
        //            item.Percentage = CalculateItemPercentage(responseItem.Files, responseItem.FileProgress);
        //            databaseContent.SetItemEta(item);
        //            break;
        //        }
        //}

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

        private double CalculateItemPercentage(IReadOnlyCollection<DownloadClientFile> files, IEnumerable<double> fileStatus)
        {
            return (fileStatus.Sum()) / files.Count;
        }
    }
}