using System;
using System.Collections.Generic;

namespace OpiSupport.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public required string Priority { get; set; } = string.Empty;  // "Baja", "Media", "Alta", "Critica"
        public required string Status { get; set; } = string.Empty;    // "Abierto", "Asignado", etc.
        public bool IsOverdue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime SLA_Deadline { get; set; }
        public int? ReopenCount { get; set; }

        // Claves foráneas
        public int CategoryId { get; set; }
        public int AreaId { get; set; }
        public int CreatedById { get; set; }
        public int? AssignedToId { get; set; }

        // Propiedades de navegación
        public Category Category { get; set; } = null!;
        public Area Area { get; set; } = null!;
        public User CreatedByUser { get; set; } = null!;
        public User AssignedToUser { get; set; } = null!;

        // Relaciones de colección
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<StatusHistory>? StatusHistories { get; set; } = new List<StatusHistory>();
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}

