using System;

namespace MyLibrary.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }


        public Book()
        {
            Title = "";
            Author = "";
        }
    }
}