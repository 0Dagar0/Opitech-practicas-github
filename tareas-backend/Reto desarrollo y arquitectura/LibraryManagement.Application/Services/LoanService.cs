using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> _loanRepository;
        private readonly IRepository<BookCopy> _copyRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IBookRepository _bookRepository;

        // Política de préstamo: 14 días de plazo
        private const int LoanDurationDays = 14;
        private const decimal FinePerDay = 1.0m;

        public LoanService(
            IRepository<Loan> loanRepository,
            IRepository<BookCopy> copyRepository,
            IRepository<User> userRepository,
            IBookRepository bookRepository)   
        {
            _loanRepository = loanRepository;
            _copyRepository = copyRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(Guid id)
        {
            return await _loanRepository.GetByIdAsync(id);
        }

        public async Task<Loan> CreateLoanAsync(Guid bookCopyId, Guid userId)
        {
                        // validación que la copia exista
            var copy = await _copyRepository.GetByIdAsync(bookCopyId);
            if (copy == null)
                throw new KeyNotFoundException($"No existe la copia con Id {bookCopyId}.");

                        // validar que el usuario existe
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"No existe el usuario con Id {userId}.");

                        // validar que la copia este disponible
            if (copy.Status != CopyStatus.Available)
                throw new InvalidOperationException($"La copia no está disponible. Estado actual: {copy.Status}.");

                        // crear el préstamo
            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                BookCopyId = bookCopyId,
                UserId = userId,
                LoanedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(LoanDurationDays),
                ReturnDate = null,
                FineAmount = 0
            };

                        //cambiar el estado de la copia a Lent
            copy.Status = CopyStatus.Lent;

            await _loanRepository.AddAsync(loan);
            _copyRepository.Update(copy);       // El contexto ya rastrea 'copy' si se obtuvo con GetByIdAsync, pero refueyrzo la actualizacion

            await _loanRepository.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> ReturnLoanAsync(Guid loanId)
        {
                                // buscar el préstamo
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new KeyNotFoundException($"No existe el préstamo con Id {loanId}.");

                                // validar que no haya sido devuelto ya
            if (loan.ReturnDate != null)
                throw new InvalidOperationException("Este préstamo ya fue devuelto.");

                                 // Registrar la devolución
            loan.ReturnDate = DateTime.UtcNow;

                                  // Calcular multa si hay retraso
            if (loan.ReturnDate > loan.DueDate)
            {
                var daysLate = (loan.ReturnDate.Value - loan.DueDate).Days;
                loan.FineAmount = daysLate * FinePerDay;
            }

                                // Liberar la copia
            var copy = await _copyRepository.GetByIdAsync(loan.BookCopyId);
            if (copy != null)
            {
                copy.Status = CopyStatus.Available;
                _copyRepository.Update(copy);
            }

            _loanRepository.Update(loan);
            await _loanRepository.SaveChangesAsync();
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserAsync(Guid userId)
        {
            return await _loanRepository.FindAsync(l => l.UserId == userId);
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _loanRepository.FindAsync(l => l.ReturnDate == null);
        }

                //metodo de libro mas prestado por categoria
        public async Task<Dictionary<string, Book?>> GetMostLoanedBookByCategoryAsync()
        {
                        // Obtener todos los préstamos y contar cuantos hay por libro
            var allLoans = await _loanRepository.GetAllAsync();
            var bookLoanCounts = new Dictionary<Guid, int>();

            foreach (var loan in allLoans)
            {
                var copy = await _copyRepository.GetByIdAsync(loan.BookCopyId);
                if (copy == null) continue;

                var bookId = copy.BookId;
                if (bookLoanCounts.ContainsKey(bookId))
                    bookLoanCounts[bookId]++;
                else
                    bookLoanCounts[bookId] = 1;
            }

                    // Por cada libro, averiguar sus categorías y quedarnos con el más prestado de cada una
            var categoryTopBook = new Dictionary<string, (Guid BookId, int Count)>();

            foreach (var bookId in bookLoanCounts.Keys)
            {
                        // Usamos el nuevo metodo del repositorio que carga las categorías
                var book = await _bookRepository.GetBookWithCategoriesAsync(bookId);
                if (book == null) continue;

                int count = bookLoanCounts[bookId];

                foreach (var category in book.Categories)
                {
                    if (!categoryTopBook.ContainsKey(category.Name) ||
                        count > categoryTopBook[category.Name].Count)
                    {
                        categoryTopBook[category.Name] = (bookId, count);
                    }
                }
            }

                    // Construir el diccionario final con los objetos Book
            var result = new Dictionary<string, Book?>();
            foreach (var entry in categoryTopBook)
            {
                var topBook = await _bookRepository.GetByIdAsync(entry.Value.BookId);
                result[entry.Key] = topBook;
            }

            return result;
        }

    }



}

