using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpiSupport.Application.DTOs;
using OpiSupport.Application.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpiSupport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
        {
            try
            {
                // Obtener el ID del usuario desde el token JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new { message = "Usuario no autenticado" });

                var ticket = await _ticketService.CreateTicketAsync(dto, userId);
                return CreatedAtAction(nameof(CreateTicket), new { id = ticket.Id }, ticket);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            // Obtener el ID del usuario desde el token JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(new { message = "Usuario no autenticado" });

            // Obtener el rol del usuario desde el token JWT
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            var role = roleClaim?.Value ?? "Colaborador"; // Por defecto Colaborador

            // 🔍 LOG TEMPORAL
            Console.WriteLine($"🧪 Usuario ID: {userId}, Rol: {role}");

            try
            {
                var tickets = await _ticketService.GetTicketsAsync(userId, role);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los tickets", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            // Obtener el ID del usuario desde el token JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(new { message = "Usuario no autenticado" });

            // Obtener el rol del usuario desde el token JWT
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            var role = roleClaim?.Value ?? "Colaborador";

            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id, userId, role);
                return Ok(ticket);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el ticket", detail = ex.Message });
            }
        }


        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            // Obtener el ID del usuario desde el token JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(new { message = "Usuario no autenticado" });

            var roleClaim = User.FindFirst(ClaimTypes.Role);
            var role = roleClaim?.Value ?? "Colaborador";

            try
            {
                var updatedTicket = await _ticketService.ChangeStatusAsync(id, dto.NewStatus, dto.Comment, userId, role);
                return Ok(updatedTicket);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cambiar el estado", detail = ex.Message });
            }
        }

        [HttpPut("{id}/assign")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> AssignTechnician(int id, [FromBody] int technicianId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(new { message = "Usuario no autenticado" });

            try
            {
                var updatedTicket = await _ticketService.AssignTechnicianAsync(id, technicianId, userId);
                return Ok(updatedTicket);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al reasignar el ticket", detail = ex.Message });
            }
        }






    }
}

