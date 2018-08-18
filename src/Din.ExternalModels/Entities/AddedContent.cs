using System;

namespace Din.ExternalModels.Entities
{

    public class AddedContent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
        public Account Account { get; set; }

        public AddedContent() { }

        public AddedContent(string title, DateTime dateAdded, ContentStatus status, Account account)
        {
            Title = title;
            DateAdded = dateAdded;
            Status = status;
            Account = account;
        }
    }

    public enum ContentStatus
    {
        NotAvailable,
        Added,
        Downloaded
    }
}
