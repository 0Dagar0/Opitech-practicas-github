using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } // este es el identificador único del libro
        public string Title { get; set; } = string.Empty;
        public string ISBN  { get; set; } = string.Empty;
        public int PublicationYear { get; set; } = 0;
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Realcion muchos a muchos
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();

    }
}
