using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RadarrMovie
{
    public class Movie
    {
        public string title { get; set; }
        public int year { get; set; }
        public int qualityProfileId { get; set; }
        public string profileId { get; set; }
        public string titleslug { get; set; }
        public List<Image> images { get; set; }
        public int tmdbid { get; set; }
        public string rootFolderPath { get; set; }
        public bool monitored { get; set; }
        public AddOptions addOptions { get; set; }

        public Movie() { }

        public Movie(string title, List<Image> images, int tmdbid, DateTime date)
        {
            this.title = title;
            this.year = date.Year;
            this.qualityProfileId = 0;
            this.profileId = "6";
            this.titleslug = GenerateTitleSlug(title, date);
            this.images = images;
            this.tmdbid = tmdbid;
            this.rootFolderPath = "F:\\Movies\\";
            this.monitored = true;
            this.addOptions = new AddOptions
            {
                searchForMovie = true
            };
        }

        private string GenerateTitleSlug(string title, DateTime date)
        {
            string slug = title.ToLower().Replace(" ", "-") + "-";
            slug = slug + date.Year.ToString().ToLower();
            return slug;
        }
    }
}
