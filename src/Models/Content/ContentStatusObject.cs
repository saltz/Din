using System;

namespace Models.Content
{
    public class ContentStatusObject
    {
        public string Title { get;  set; }
        public string Status { get;  set; }
        public string AccountName { get; private set; }
        public int Eta { get; set; }
        public DateTime DateAdded { get; private set; }


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
    }
}
