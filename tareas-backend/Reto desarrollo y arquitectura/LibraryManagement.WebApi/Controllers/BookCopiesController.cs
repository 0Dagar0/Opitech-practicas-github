using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCopiesController : ControllerBase
    {
        private readonly IBookCopyService _copyService;

        public BookCopiesController(IBookCopyService copyService)
        {
            _copyService = copyService;
        }

                    // GET: api/bookccopies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetAll()
        {
            var copies = await _copyService.GetAllCopiesAsync();
            return Ok(copies);
        }

                    // GET: api/bookcopies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCopy>> GetById(Guid id)
        {
            var copy = await _copyService.GetCopyByIdAsync(id);
            if (copy == null)
                return NotFound();
            return Ok(copy);
        }

                    // GET: api/bookcopies/book/{bookId}  Obtener todas las copias de un libro específico
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetByBookId(Guid bookId)
        {
            var copies = await _copyService.GetCopiesByBookIdAsync(bookId);
            return Ok(copies);
        }

                    // POST: api/bookcopies
        [HttpPost]
        public async Task<ActionResult<BookCopy>> Create([FromBody] CreateBookCopyRequest request)
        {
            var copy = new BookCopy
            {
                Id = Guid.NewGuid(),
                Barcode = request.Barcode,
                BookId = request.BookId,
                Status = request.Status,
                AcquisitionDate = request.AcquisitionDate ?? DateTime.UtcNow
            };

            var created = await _copyService.CreateCopyAsync(copy);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

                    // PUT: api/bookcopies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCopyRequest request)
        {
            var existing = await _copyService.GetCopyByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Barcode = request.Barcode;
            existing.Status = request.Status;
            await _copyService.UpdateCopyAsync(existing);
            return NoContent();
        }

                // DELETE: api/bookcopies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _copyService.DeleteCopyAsync(id);
            return NoContent();
        }

                
        [HttpGet("list")]
        [ProducesResponseType(typeof(PagedResult<BookCopy>), 200)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _copyService.GetPagedCopiesAsync(page, pageSize);
            return Ok(result);
        }


    }
}