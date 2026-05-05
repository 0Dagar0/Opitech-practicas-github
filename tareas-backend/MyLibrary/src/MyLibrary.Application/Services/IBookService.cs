using MyLibrary.Application.DTOs;
using MyLibrary.Domain.Entities;

namespace MyLibrary.Application.Services;

public interface IBookService
{
    Book AddBook(BookDTO bookDTO);
    List<Book> GetBooks();
}