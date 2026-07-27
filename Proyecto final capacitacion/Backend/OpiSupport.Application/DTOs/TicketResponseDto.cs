using System;

namespace OpiSupport.Application.DTOs
{
    public class TicketResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public required string Category { get; set; }
        public required string Area { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public required string CreatedByFullName { get; set; }
    }
}

