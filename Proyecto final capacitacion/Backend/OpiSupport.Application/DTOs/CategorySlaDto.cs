namespace OpiSupport.Application.DTOs
{
    public class CategorySlaDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int TotalResolved { get; set; }
        public int Compliant { get; set; }
        public int NonCompliant { get; set; }
        public double CompliancePercentage { get; set; }
    }
}