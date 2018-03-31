using System;
using System.Collections.Generic;
using System.Text;
using DinWebsite.ExternalModels.Authentication;

namespace DinWebsite.ExternalModels.Content
{
    public class AddedMovies
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public User User { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
    }

    public enum ContentStatus
    {
        Added,
        NotAvailable,
        Downloaded
    }
}
