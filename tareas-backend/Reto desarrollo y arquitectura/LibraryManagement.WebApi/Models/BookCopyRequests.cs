using LibraryManagement.Domain.Entities;

namespace LibraryManagement.WebApi.Models
{
    public class CreateBookCopyRequest
    {
        public string Barcode { get; set; } = string.Empty;
        public Guid BookId { get; set; }
        public CopyStatus Status { get; set; } = CopyStatus.Available;
        public DateTime? AcquisitionDate { get; set; }
    }

    public class UpdateBookCopyRequest
    {
        public string Barcode { get; set; } = string.Empty;
        public CopyStatus Status { get; set; } = CopyStatus.Available;
    }
}
