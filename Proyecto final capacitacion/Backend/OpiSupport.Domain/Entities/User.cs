using System;
using System.Collections.Generic;

namespace OpiSupport.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  // Puede ser null, pero lo dejamos como string vacío por defecto
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;   // "Colaborador", "Tecnico", "Supervisor"
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ActiveReopenedCount { get; set; }

        // Relaciones (Navegación) - Inicializamos con listas vacías
        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<StatusHistory> StatusChanges { get; set; } = new List<StatusHistory>();
        public ICollection<TechnicianSpecialty> Specialties { get; set; } = new List<TechnicianSpecialty>();
    }
}


