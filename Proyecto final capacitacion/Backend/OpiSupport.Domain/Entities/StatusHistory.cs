using System;

namespace OpiSupport.Domain.Entities
{
    public class StatusHistory
    {
        public int Id { get; set; }
        public string? PreviousStatus { get; set; } = string.Empty;  // Puede ser null si es la creación
        public string NewStatus { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }

        // Claves foráneas (FK)
        public int TicketId { get; set; }
        public int ChangedByUserId { get; set; }

        // Propiedades de navegación (relaciones)
        public Ticket Ticket { get; set; } = null!;
        public User ChangedByUser { get; set; } = null!;
    }
}

