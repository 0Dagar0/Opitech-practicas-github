using System;
using System.Collections.Generic;


namespace LibraryManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // para autenticación 
        public UserRole Role { get; set; } = UserRole.Reader; 

        // relaciones: un usuari puede tener varios prestamos y reservas 
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }

    public enum  UserRole
    {
        Admin,
        Reader
    }

}
