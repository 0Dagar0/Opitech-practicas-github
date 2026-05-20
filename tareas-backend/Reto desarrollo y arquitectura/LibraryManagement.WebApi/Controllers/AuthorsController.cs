using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(Guid id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Create([FromBody] Author author)
        {
            var created = await _authorService.CreateAuthorAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Author author)
        {
            if (id != author.Id)
                return BadRequest("Id mismatch");
            await _authorService.UpdateAuthorAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }

                // GET: api/authors/list? page = 1  pageSize = 10
        [HttpGet("list")]
        [ProducesResponseType(typeof(PagedResult<Author>), 200)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _authorService.GetPagedAuthorsAsync(page, pageSize);
            return Ok(result);
        }


    }
}