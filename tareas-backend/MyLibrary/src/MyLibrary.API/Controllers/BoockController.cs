using Microsoft.AspNetCore.Mvc;
using MyLibrary.Application.DTOs;
using MyLibrary.Application.Services;

namespace MyLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDTO)
        {
            var book = _bookService.AddBook(bookDTO);
            return Ok(book);
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }
    }
}
