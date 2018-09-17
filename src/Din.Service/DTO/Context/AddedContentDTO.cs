using System;
using Din.Data.Entities;

namespace Din.Service.DTO.Context
{
    public class AddedContentDTO
    {
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
    }
}
