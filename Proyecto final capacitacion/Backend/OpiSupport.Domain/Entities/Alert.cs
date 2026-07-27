using System;

namespace OpiSupport.Domain.Entities
{
    public class Alert
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Propiedad de navegación (relación)
        public Ticket Ticket { get; set; } = null!;
    }
}


