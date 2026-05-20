namespace LibraryManagement.WebApi.Models
{
    public class CreateReservationRequest
    {
        public Guid BookCopyId { get; set; }
        public Guid UserId { get; set; }
    }
}

// Solo necesitamos un DTO para crear, porque la cancelación solo usa el ID en la URL.