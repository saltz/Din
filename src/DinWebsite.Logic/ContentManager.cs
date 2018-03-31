using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using DinWebsite.ExternalModels.Content;
using DinWebsite.ExternalModels.DownloadClient;
using DinWebsite.Logic.TMDB;
using TMDbLib.Objects.Search;

namespace DinWebsite.Logic
{
    public class ContentManager
    {
        private TmdbSystem _tmdbSystem;
        private MediaSystem.MediaSystem _mediaSystem;
        private DownloadSystem.DownloadSystem _downloadSystem;
        private readonly Properties _properties;

        public ContentManager()
        {
            _properties = new Properties("C:/din_properties/properties");
        }

        public string GenerateBackground()
        {
            var url = _properties.Get("unsplash");
            string result;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        public string GetRandomGif(GiphyQuery query)
        {
            string url;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    url = _properties.Get("giphyPageNotFound");
                    break;
                case GiphyQuery.Forbidden:
                    url = _properties.Get("giphyForbidden");
                    break;
                case GiphyQuery.Logout:
                    url = _properties.Get("giphyLogout");
                    break;
                case GiphyQuery.ServerError:
                    url = _properties.Get("giphyServerError");
                    break;
                default:
                    url = _properties.Get("giphyRandom");
                    break;
            }
            string result;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public List<SearchMovie> TmdbSearchMovie(string searchQuery)
        {
            _tmdbSystem = new TmdbSystem(_properties.Get("tmdb"));
            return _tmdbSystem.SearchMovie(searchQuery);
        }

        public string MediaSystemAddMovie(SearchMovie movie, Object userAdObject)
        {
            _mediaSystem = new MediaSystem.MediaSystem(_properties.Get("mediaSystem"));
            var status = _mediaSystem.AddMovie(movie);
            if (!status.Equals("error"))
            {
                // ReportEvent(userAdObject, movie.Title, "added"); SUCCESS
                return status;
            }
            //  ReportEvent(userAdObject, movie.Title, "failed to add"); FAILED
            return status;
        }

        public List<int> MediaSystemGetCurrentMovies()
        {
            _mediaSystem = new MediaSystem.MediaSystem(_properties.Get("mediaSystem"));
            return _mediaSystem.GetCurrentMovies();
        }

        //public static List<ContentStatusObject> GetContentStatus(Object user)
        //{
        //    //UpdateContentStatus(user);
        //    //var databaseContent = new DatabaseContent();
        //    //return databaseContent.GetMoviesByAccountname(user.SAMAccountName);
        //}


        //private static void UpdateContentStatus(ADObject user)
        //{
        //    //var databaseContent = new DatabaseContent();
        //    //var movies = databaseContent.GetMoviesByAccountname(user.SAMAccountName);
        //    //var itemsToCheck = movies.Where(cso => !cso.Status.Equals("downloaded")).ToList();
        //    //itemsToCheck = CheckIfItemIsCompleted(itemsToCheck);
        //    //if (!DownloadClient.Authenticate()) return;
        //    //var downloadClientItems = DownloadClient.GetAllItems();
        //    //foreach (var item in itemsToCheck)
        //    //foreach (var dItem in downloadClientItems)
        //    //{
        //    //    var titles = FixNames(item.Title, dItem.Name);
        //    //    if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
        //    //    var responseItem = DownloadClient.GetItemStatus(dItem.Hash);
        //    //    item.Eta = responseItem.Eta;
        //    //    item.Percentage = CalculateItemPercentage(responseItem.Files, responseItem.FileProgress);
        //    //    databaseContent.SetItemEta(item);
        //    //    break;
        //    //}
        //}

        //private static List<ContentStatusObject> CheckIfItemIsCompleted(List<ContentStatusObject> items)
        //{
        //    //var databaseContent = new DatabaseContent();
        //    //string webResult;

        //    //var httpWebRequest = (HttpWebRequest) WebRequest.Create(ApiMovieRequest);
        //    //httpWebRequest.ContentType = "application/json";
        //    //httpWebRequest.Method = "GET";
        //    //var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        //    //using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
        //    //{
        //    //    webResult = streamReader.ReadToEnd();
        //    //}
        //    //var objects = JsonConvert.DeserializeObject<List<Movie>>(webResult);
        //    //foreach (var i in items)
        //    //foreach (var m in objects)
        //    //{
        //    //    if (!i.Title.Equals(m.Title)) continue;
        //    //    if (!m.Downloaded) continue;
        //    //    i.Status = "downloaded";
        //    //   // databaseContent.UpdateItemStatus(i);
        //    //    break;
        //    //}
        //    //return items;
        //}

        //private void ReportEvent(ADObject userAdObject, string movieTitle, string status)
        //{
        //    //var databaseContent = new DatabaseContent();
        //    //databaseContent.InsertMovieAddedData(userAdObject, movieTitle, status);
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