using System.Threading.Tasks;
using OpiSupport.Application.DTOs;

namespace OpiSupport.Application.Interfaces
{
    public interface ITicketService
    {
        Task<TicketResponseDto> CreateTicketAsync(CreateTicketDto dto, int userId);
        Task<List<TicketListDto>> GetTicketsAsync(int userId, string role);
        Task<TicketDetailDto> GetTicketByIdAsync(int ticketId, int userId, string role);
        Task<TicketDetailDto> ChangeStatusAsync(int ticketId, string newStatus, string? comment, int userId, string role);
        Task<TicketDetailDto> AssignTechnicianAsync(int ticketId, int technicianId, int userId);
        Task<SlaReportDto> GetSlaReportAsync();

    }
}

