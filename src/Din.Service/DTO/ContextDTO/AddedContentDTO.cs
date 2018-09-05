using System;

namespace Din.Service.DTO.ContextDTO
{
    public class AddedContentDTO
    {
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        internal ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
    }

    internal enum ContentStatus
    {
        NotAvailable,
        Added,
        Downloaded
    }
}
