using System.Collections.Generic;

namespace OpiSupport.Application.DTOs
{
    public class SlaReportDto
    {
        public double GlobalCompliancePercentage { get; set; }
        public List<TechnicianSlaDto> Technicians { get; set; } = new();
        public List<CategorySlaDto> Categories { get; set; } = new();
    }
}