using System;

namespace DinWebsite.ExternalModels.Content
{
    public class ContentStatusObject
    {
        public ContentStatusObject(string title, string status, string accountName, DateTime dateAdded)
        {
            Title = title;
            Status = status;
            AccountName = accountName;
            DateAdded = dateAdded;
        }

        public ContentStatusObject(string title, string status, string accountName, int eta, DateTime dateAdded)
        {
            Title = title;
            Status = status;
            AccountName = accountName;
            Eta = eta;
            DateAdded = dateAdded;
        }

        public ContentStatusObject(string title, string status, string accountName, int eta, double percentage, DateTime dateAdded)
        {
            Title = title;
            Status = status;
            AccountName = accountName;
            Eta = eta;
            Percentage = percentage;
            DateAdded = dateAdded;
        }

        public string Title { get; set; }
        public string Status { get; set; }
        public string AccountName { get; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
        public DateTime DateAdded { get; }
    }
}