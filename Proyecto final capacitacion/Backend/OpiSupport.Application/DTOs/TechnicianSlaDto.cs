namespace OpiSupport.Application.DTOs
{
    public class TechnicianSlaDto
    {
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; } = string.Empty;
        public int TotalResolved { get; set; }
        public int Compliant { get; set; }
        public int NonCompliant { get; set; }
        public double CompliancePercentage { get; set; }
    }
}