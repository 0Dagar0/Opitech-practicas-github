using System;

namespace OpiSupport.Application.DTOs
{
    public class TicketListDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public required string Category { get; set; }
        public required string Area { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? AssignedToFullName { get; set; } // Puede ser null si no está asignado
        public required string CreatedByFullName { get; set; }
        public bool IsOverdue { get; set; }
    }
}