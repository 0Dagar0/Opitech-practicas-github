using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Models
{
    public class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> AuthorIds { get; set; } = new();
        public List<Guid> CategoryIds { get; set; } = new();
    }

    public class UpdateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> AuthorIds { get; set; } = new();
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
