using System;

namespace OpiSupport.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Claves foráneas (FK)
        public int TicketId { get; set; }
        public int UserId { get; set; }

        // Propiedades de navegación (relaciones)
        public Ticket? Ticket { get; set; }
        public User? User { get; set; }
    }
}
