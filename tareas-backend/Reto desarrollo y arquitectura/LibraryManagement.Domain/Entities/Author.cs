using System;


namespace LibraryManagement.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
