using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;


        // relación: una reserva pertenece a un usuario y a un ejemplar de libro
        public Guid BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

    }

    public enum ReservationStatus
    {
        Active,
        Completed,
        Cancelled
    }

}
