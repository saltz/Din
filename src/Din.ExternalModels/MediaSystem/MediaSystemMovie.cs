using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.ExternalModels.MediaSystem
{
    public class MediaSystemMovie
    {
        public MediaSystemMovie()
        {
        }

        public MediaSystemMovie(string title, List<MovieImage> images, int tmdbid, DateTime date)
        {
            Title = title;
            Year = date.Year;
            QualityProfileId = 0;
            ProfileId = "6";
            Titleslug = GenerateTitleSlug(title, date);
            Images = images;
            Tmdbid = tmdbid;
            RootFolderPath = "F:\\Movies\\";
            Monitored = true;
            MovieOptions = new MovieOptions
            {
                SearchForMovie = true
            };
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("qualityProfileId")]
        public int QualityProfileId { get; set; }

        [JsonProperty("profileId")]
        public string ProfileId { get; set; }

        [JsonProperty("titleslug")]
        public string Titleslug { get; set; }

        [JsonProperty("images")]
        public List<MovieImage> Images { get; set; }

        [JsonProperty("tmdbid")]
        public int Tmdbid { get; set; }

        [JsonProperty("rootFolderPath")]
        public string RootFolderPath { get; set; }

        [JsonProperty("monitored")]
        public bool Monitored { get; set; }

        [JsonProperty("addOptions")]
        public MovieOptions MovieOptions { get; set; }

        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }

        private string GenerateTitleSlug(string title, DateTime date)
        {
            var slug = title.ToLower().Replace(" ", "-") + "-";
            slug = slug + date.Year.ToString().ToLower();
            return slug;
        }
    }

    public class MovieImage
    {
        public MovieImage()
        {
        }

        public MovieImage(string url)
        {
            Covertype = "poster";
            Url = url;
        }

        [JsonProperty("covertype")]
        public string Covertype { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MovieOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")]
        public bool IgnoreEpisodesWithFiles { get; set; }

        [JsonProperty("ignoreEpisodesWithoutFiles")]
        public bool IgnoreEpisodesWithoutFiles { get; set; }

        [JsonProperty("searchForMovie")]
        public bool SearchForMovie { get; set; }
    }
}