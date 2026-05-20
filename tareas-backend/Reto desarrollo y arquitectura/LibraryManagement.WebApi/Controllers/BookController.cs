using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.WebApi.Models;

namespace LibraryManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

                // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

             // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

                // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] CreateBookRequest request )
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                ISBN = request.ISBN,
                PublicationYear = request.PublicationYear,
                Publisher = request.Publisher,
                Description = request.Description,
            };

            var created = await _bookService.CreateBookAsync(book, request.AuthorIds, request.CategoryIds);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

                // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookRequest request)
        {
            var book = new Book
            {
                Id = id,
                Title = request.Title,
                ISBN = request.ISBN,
                PublicationYear = request.PublicationYear,
                Publisher = request.Publisher,
                Description = request.Description,
            };



            await _bookService.UpdateBookAsync(book, request.AuthorIds, request.CategoryIds);
            return NoContent();
        }

                // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

                // GET: api/books/author/{authorId}
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByAuthor(Guid authorId)
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId);
            return Ok(books);
        }

                // GET: api/books/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByCategory(Guid categoryId)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryId);
            return Ok(books);
        }

                // GET: api/books/paged page=1 y page Size=10
        [HttpGet("list")]
        public async Task<ActionResult<PagedResult<Book>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _bookService.GetPagedBooksAsync(page, pageSize);
            return Ok(result);
        }
    }
}