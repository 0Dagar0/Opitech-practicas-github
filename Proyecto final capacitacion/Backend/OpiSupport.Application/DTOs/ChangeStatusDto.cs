using System;

namespace OpiSupport.Application.DTOs
{
    public class ChangeStatusDto
    {
        public required string NewStatus { get; set; } // "En Proceso", "Resuelto", "Cerrar", "Reabierto"
        public string? Comment { get; set; } // Opcional, pero obligatorio si NewStatus = "Resuelto"
    }
}



