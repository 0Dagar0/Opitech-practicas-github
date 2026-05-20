using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(Guid id);
        Task<Loan> CreateLoanAsync(Guid bookCopyId, Guid userId);
        Task<Loan> ReturnLoanAsync(Guid loanId);
        Task<IEnumerable<Loan>> GetLoansByUserAsync(Guid userId); // es para ver el historial de prestamos de un usuario
        Task<IEnumerable<Loan>> GetActiveLoansAsync(); // es para ver los prestamos activos, los que no han sido devueltos
        Task<Dictionary<string, Book?>> GetMostLoanedBookByCategoryAsync(); // es para obtener el libro más prestado por categoria,
                                                                           // devuelve un diccionario donde la clave es la categoria y
                                                                           // el valor es el libro más prestado de esa categoria
    }
}

