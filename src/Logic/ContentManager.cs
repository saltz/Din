using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Database;
using Models.AD;
using Models.Content;
using Models.DownloadClient;
using Models.Movie;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Logic
{
    public static class ContentManager
    {
        private static readonly TMDbClient TmDbClient = new TMDbClient(File.ReadLines("C:/din_properties/api_key").First());
        private static readonly DownloadClient DownloadClient = new DownloadClient();
        private static readonly string ApiMovieRequest = File.ReadLines("C:/din_properties/api_movie").First();

        public static List<SearchMovie> SearchMovie(string searchQuery)
        {
            SearchContainer<SearchMovie> movies = TmDbClient.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }

        public static string AddMovie(SearchMovie movie, ADObject userAdObject)
        {
            List<Image> images = new List<Image>();
            images.Add(new Image(movie.PosterPath));
            var payload = new Movie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiMovieRequest);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                ReportEvent(userAdObject, movie.Title, "added");
                return httpResponse.StatusDescription;
            }
            catch (WebException)
            {
                ReportEvent(userAdObject, movie.Title, "failed to add");
                return "error";
            }
        }

        public static List<int> GetCurrentMovies()
        {
            List<int> movieIds = new List<int>();
            string webResult;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiMovieRequest);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                webResult = streamReader.ReadToEnd();
            }
            List<Movie> objects = JsonConvert.DeserializeObject<List<Movie>>(webResult);
            foreach (Movie m in objects)
            {
                movieIds.Add(m.Tmdbid);
            }
            return movieIds;
        }

        public static List<ContentStatusObject> GetContentStatus(ADObject user)
        {
            UpdateContentStatus(user);
            var databaseContent = new DatabaseContent();
            return databaseContent.GetMoviesByAccountname(user.SAMAccountName);
        }

        private static void ReportEvent(ADObject userAdObject, string movieTitle, string status)
        {
            DatabaseContent databaseContent = new DatabaseContent();
            databaseContent.InsertMovieAddedData(userAdObject, movieTitle, status);
        }

        private static void UpdateContentStatus(ADObject user)
        {
            var databaseContent = new DatabaseContent();
            var movies = databaseContent.GetMoviesByAccountname(user.SAMAccountName);
            var itemsToCheck = movies.Where(cso => !cso.Status.Equals("downloaded")).ToList();
            itemsToCheck = CheckIfItemIsCompleted(itemsToCheck);
            DownloadClient.Authenticate();
            List<DownloadClientItem> downloadClientItems = DownloadClient.GetAllItems();
            foreach (var item in itemsToCheck)
            {
                foreach (var dItem in downloadClientItems)
                {
                    string[] titles = FixNames(item.Title, dItem.Name);
                    if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
                    item.Eta = DownloadClient.GetItemEta(dItem.Hash);
                    databaseContent.SetItemEta(item);
                    break;
                }
            }
        }

        private static List<ContentStatusObject> CheckIfItemIsCompleted(List<ContentStatusObject> items)
        {
            DatabaseContent databaseContent = new DatabaseContent();
            string webResult;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiMovieRequest);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                webResult = streamReader.ReadToEnd();
            }
            List<Movie> objects = JsonConvert.DeserializeObject<List<Movie>>(webResult);
            foreach (var i in items)
            {
                foreach (var m in objects)
                {
                    if (!i.Title.Equals(m.Title)) continue;
                    if (!m.Downloaded) continue;
                    i.Status = "downloaded";
                    databaseContent.UpdateItemStatus(i);
                    break;
                }
            }
            return items;
        }

        private static string[] FixNames(string title1, string title2)
        {
            title1 = title1.Replace(" ", ".");
            if (title2.Contains("1080p"))
                title2 = title2.Substring(0, title2.IndexOf("1080p", StringComparison.Ordinal));

            if (title2.Contains("720p"))
                title2 = title2.Substring(0,title2.IndexOf("720p", StringComparison.Ordinal));

            title1 = Regex.Replace(title1, @"[\d-]", string.Empty);
            title2 = Regex.Replace(title2, @"[\d-]", string.Empty);
            return new[] {title1, title2};
        }
    }
}
