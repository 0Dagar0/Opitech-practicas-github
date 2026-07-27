using System.Collections.Generic;

namespace OpiSupport.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;

        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        public ICollection<TechnicianSpecialty> TechnicianSpecialties { get; set; } = new List<TechnicianSpecialty>();
    }
}



