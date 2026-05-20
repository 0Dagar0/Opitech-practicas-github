using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

                    // GET: api/loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetAll()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

                             // GET: api/loans/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetById(Guid id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
                return NotFound();
            return Ok(loan);
        }

                          // POST: api/loans
        [HttpPost]
        public async Task<ActionResult<Loan>> Create([FromBody] CreateLoanRequest request)
        {
            var loan = await _loanService.CreateLoanAsync(request.BookCopyId, request.UserId);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

                    // PUT: api/loans/{id}/return
        [HttpPut("{id}/return")]
        public async Task<ActionResult<Loan>> Return(Guid id)
        {
            var loan = await _loanService.ReturnLoanAsync(id);
            return Ok(loan);
        }

                            // GET: api/loans/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetByUser(Guid userId)
        {
            var loans = await _loanService.GetLoansByUserAsync(userId);
            return Ok(loans);
        }

                             // GET: api/loans/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetActive()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(loans);
        }

                // GET: api/loans/most-loaned-by-category
        [HttpGet("most-loaned-by-category")]
        public async Task<ActionResult<Dictionary<string, Book?>>> GetMostLoanedByCategory()
        {
            var result = await _loanService.GetMostLoanedBookByCategoryAsync();
            return Ok(result);      // devuelve un diccionario con el nombre de la categoría y el libro mas prestado en ella.
        }


    }
}