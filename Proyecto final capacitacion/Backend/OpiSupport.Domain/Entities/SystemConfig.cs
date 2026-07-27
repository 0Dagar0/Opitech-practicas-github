namespace OpiSupport.Domain.Entities
{
    public class SystemConfig
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;  // Ej: "MaxTicketsPerTechnician"
        public string Value { get; set; } = string.Empty; // Ej: "5"
    }
}

