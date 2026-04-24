using Microsoft.AspNetCore.Mvc;
using ServiciosEInyeccionDependencias.Application.Dtos;
using ServiciosEInyeccionDependencias.Application.Services;
using System.Threading.Tasks;

namespace ServiciosEInyeccionDependencias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        // Constructor: recibe BookService por inyección de dependencias
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound($"No se encontró el libro con ID {id}");

            return Ok(book);
        }

        // GET: api/books/author/Gabriel
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {
            var books = await _bookService.GetBooksByAuthorAsync(author);
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBook = await _bookService.CreateBookAsync(createBookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, updateBookDto);
                if (updatedBook == null)
                    return NotFound($"No se encontró el libro con ID {id}");

                return Ok(updatedBook);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted)
                return NotFound($"No se encontró el libro con ID {id}");

            return NoContent();
        }
    }
}