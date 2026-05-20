using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public DateTime LoanedDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // nulo si no lo ha devuelto aun
        public decimal FineAmount { get; set; }


        public Guid BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;


    }
}
