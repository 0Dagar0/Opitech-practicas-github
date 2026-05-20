using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IRepository<BookCopy> _copyRepository;
        private readonly IRepository<Book> _bookRepository;  // Para validar que el libro exissta

        public BookCopyService(IRepository<BookCopy> copyRepository, IRepository<Book> bookRepository)
        {
            _copyRepository = copyRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookCopy>> GetAllCopiesAsync()
        {
            return await _copyRepository.GetAllAsync();
        }

        public async Task<BookCopy?> GetCopyByIdAsync(Guid id)
        {
            return await _copyRepository.GetByIdAsync(id);
        }

        public async Task<BookCopy> CreateCopyAsync(BookCopy copy)
        {
            //  Validar que el libro asociado exista
            var book = await _bookRepository.GetByIdAsync(copy.BookId);
            if (book == null)
                throw new KeyNotFoundException($"No existe el libro con Id {copy.BookId}.");

            //  Asignar valores por defecto y forzar UTC
            if (copy.AcquisitionDate == default)
                copy.AcquisitionDate = DateTime.UtcNow;
            else
                // Aseguramos que cualquier fecha enviada se convierta a UTC
                copy.AcquisitionDate = DateTime.SpecifyKind(copy.AcquisitionDate, DateTimeKind.Utc);

            if (copy.Status == default)
                copy.Status = CopyStatus.Available;

            await _copyRepository.AddAsync(copy);
            await _copyRepository.SaveChangesAsync();
            return copy;
        }

        public async Task UpdateCopyAsync(BookCopy copy)
        {
            var existing = await _copyRepository.GetByIdAsync(copy.Id);
            if (existing == null)
                throw new KeyNotFoundException($"No existe la copia con Id {copy.Id}.");

            // Solo se permite cambiar Barcode y Sstatus
            existing.Barcode = copy.Barcode;
            existing.Status = copy.Status;

            _copyRepository.Update(existing);
            await _copyRepository.SaveChangesAsync();
        }

        public async Task DeleteCopyAsync(Guid id)
        {
            var copy = await _copyRepository.GetByIdAsync(id);
            if (copy != null)
            {
                _copyRepository.Delete(copy);
                await _copyRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookCopy>> GetCopiesByBookIdAsync(Guid bookId)
        {
            return await _copyRepository.FindAsync(c => c.BookId == bookId);
        }

        public async Task<PagedResult<BookCopy>> GetPagedCopiesAsync(int page, int pageSize)
        {
            return await _copyRepository.GetPagedAsync(page, pageSize);
        }


    }
}