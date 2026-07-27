namespace OpiSupport.Domain.Entities
{
    public class TechnicianSpecialty
    {
        public int Id { get; set; }

        // Claves foráneas (FK)
        public int TechnicianId { get; set; }  // Apunta a User (rol Técnico)
        public int CategoryId { get; set; }    // Apunta a Category

        // Propiedades de navegación (relaciones)
        public User Technician { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
