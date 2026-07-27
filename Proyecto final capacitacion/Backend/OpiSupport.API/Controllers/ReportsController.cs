using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpiSupport.Application.Interfaces;
using System.Threading.Tasks;

namespace OpiSupport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Supervisor")] // Solo supervisores pueden ver el reporte
    public class ReportsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public ReportsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("sla-compliance")]
        public async Task<IActionResult> GetSlaCompliance()
        {
            try
            {
                var report = await _ticketService.GetSlaReportAsync();
                return Ok(report);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Error al generar el reporte SLA", detail = ex.Message });
            }
        }
    }
}

