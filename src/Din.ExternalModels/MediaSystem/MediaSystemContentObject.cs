using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.MediaSystem
{
    public abstract class MediaSystemContentObject
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("qualityProfileId")]
        public int QualityProfileId { get; set; }
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }
        [JsonProperty("titleslug")]
        public string Titleslug { get; set; }
        [JsonProperty("images")]
        public List<MediaSystemImage> Images { get; set; }
        [JsonProperty("rootFolderPath")]
        public string RootFolderPath { get; set; }
        [JsonProperty("monitored")]
        public bool Monitored { get; set; }

        protected string GenerateTitleSlug(string title, DateTime date)
        {
            var slug = title.ToLower().Replace(" ", "-") + "-";
            slug = slug + date.Year.ToString().ToLower();
            return slug;
        }
    }

    public class MediaSystemMovie : MediaSystemContentObject
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("tmdbid")]
        public int Tmdbid { get; set; }
        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }
        [JsonProperty("addOptions")]
        public MesiaSystemOptions MovieOptions { get; set; }

        public MediaSystemMovie() { }

        public MediaSystemMovie(string title, DateTime date, int tmdbid, List<MediaSystemImage> images, string fileLocation)
        {
            Title = title;
            Year = date.Year;
            QualityProfileId = 0;
            ProfileId = "6";
            Titleslug = GenerateTitleSlug(title, date);
            Images = images;
            Tmdbid = tmdbid;
            RootFolderPath = fileLocation;
            Monitored = true;
            MovieOptions = new MesiaSystemOptions
            {
                SearchForMovie = true
            };
        }
    }

    public class MediaSystemTvShow : MediaSystemContentObject
    {
        [JsonProperty("tvdbid")]
        public string TvShowId { get; set; }
        [JsonProperty("seasons")]
        public List<TvShowSeason> Seasons { get; set; }

        public MediaSystemTvShow() { }

        public MediaSystemTvShow(string title, DateTime date, string id, List<MediaSystemImage> images, IEnumerable<SearchTvSeason> seasons, string fileLocation)
        {
            TvShowId = id;
            Title = title;
            QualityProfileId = 0;
            ProfileId = "6";
            Titleslug = GenerateTitleSlug(title, date);
            Images = images;
            RootFolderPath = fileLocation;
            Monitored = true;
            Seasons = new List<TvShowSeason>();
            foreach (var s in seasons)
                Seasons.Add(new TvShowSeason(s.SeasonNumber.ToString(), true));
        }
    }

    public class MediaSystemImage
    {
        public MediaSystemImage()
        {
        }

        public MediaSystemImage(string coverType, string url)
        {
            Covertype = coverType;
            Url = url;
        }

        [JsonProperty("covertype")]
        public string Covertype { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MesiaSystemOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")]
        public bool IgnoreEpisodesWithFiles { get; set; }

        [JsonProperty("ignoreEpisodesWithoutFiles")]
        public bool IgnoreEpisodesWithoutFiles { get; set; }

        [JsonProperty("searchForMovie")]
        public bool SearchForMovie { get; set; }
    }

    public class TvShowSeason
    {
        [JsonProperty("seasonNumber")]
        public string SeasonNumber { get; set; }
        [JsonProperty("monitored")]
        public bool Monitored { get; set; }

        public TvShowSeason() { }

        public TvShowSeason(string seasonNumber, bool monitored)
        {
            SeasonNumber = seasonNumber;
            Monitored = monitored;
        }
    }
}