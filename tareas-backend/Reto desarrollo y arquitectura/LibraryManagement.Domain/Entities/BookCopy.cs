using System;
using System.Collections.Generic;


namespace LibraryManagement.Domain.Entities
{
    public class BookCopy
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public DateTime AcquisitionDate { get; set; } = DateTime.Now;
        public CopyStatus Status { get; set; } = CopyStatus.Available;

        // realción un ejemplar pwrtenece a un libro 
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

    }

    // enumeración para el estado del ejemplar 
    public enum CopyStatus
    {
        Available,
        Lent,
        CheckedOut,
        Reserved,
        Lost,
        Damaged
    }
}
