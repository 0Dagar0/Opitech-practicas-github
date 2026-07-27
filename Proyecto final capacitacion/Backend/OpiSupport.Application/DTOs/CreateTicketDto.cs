namespace OpiSupport.Application.DTOs
{
    public class CreateTicketDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public int AreaId { get; set; }
        public required string Priority { get; set; } // "Baja", "Media", "Alta", "Critica"
    }
}

