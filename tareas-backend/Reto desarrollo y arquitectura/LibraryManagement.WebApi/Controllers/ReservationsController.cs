using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

                    // GET: api/reservationns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAll()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

                        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetById(Guid id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

                     // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> Create([FromBody] CreateReservationRequest request)
        {
            var reservation = await _reservationService.CreateReservationAsync(request.BookCopyId, request.UserId);
            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

                        // PUT: api/reservations/{id}/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _reservationService.CancelReservationAsync(id);
            return NoContent();
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(PagedResult<Reservation>), 200)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _reservationService.GetPagedReservationsAsync(page, pageSize);
            return Ok(result);
        }


    }
}