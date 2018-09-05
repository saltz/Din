using System;

namespace Din.Data.Entities
{

    public class AddedContentEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        internal ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
        public AccountEntity Account { get; set; }
    }

    internal enum ContentStatus
    {
        NotAvailable,
        Added,
        Downloaded
    }
}
