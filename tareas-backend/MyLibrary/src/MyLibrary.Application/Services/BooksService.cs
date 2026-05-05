using System;
using System.Net.Security;
using MyLibrary.Application.DTOs;
using MyLibrary.Domain.Entities;
using MyLibrary.Application.Services;
namespace MyLibrary.Application.Services;

public class BooksService : IBookService 
{
    private List<Book> _books;
    public BooksService()
    {
        _books = new List<Book>();
    }

    public Book AddBook(BookDTO bookDTO)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = bookDTO.Title,
            Author = bookDTO.Author
        };

        _books.Add(book);

        return book;
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

}


