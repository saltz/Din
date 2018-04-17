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
        public Account Account { get; set; }
    }

    public enum ContentStatus
    {
        Added,
        NotAvailable,
        Downloaded
    }
}
