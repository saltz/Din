using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.Movie
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(string title, List<Image> images, int tmdbid, DateTime date)
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
            AddOptions = new AddOptions
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
        public List<Image> Images { get; set; }

        [JsonProperty("tmdbid")]
        public int Tmdbid { get; set; }

        [JsonProperty("rootFolderPath")]
        public string RootFolderPath { get; set; }

        [JsonProperty("monitored")]
        public bool Monitored { get; set; }

        [JsonProperty("addOptions")]
        public AddOptions AddOptions { get; set; }

        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }

        private string GenerateTitleSlug(string title, DateTime date)
        {
            var slug = title.ToLower().Replace(" ", "-") + "-";
            slug = slug + date.Year.ToString().ToLower();
            return slug;
        }
    }
}