using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopy>> GetAllCopiesAsync();
        Task<BookCopy?> GetCopyByIdAsync(Guid id);
        Task<BookCopy> CreateCopyAsync(BookCopy copy);
        Task UpdateCopyAsync(BookCopy copy);
        Task DeleteCopyAsync(Guid id);
        Task<IEnumerable<BookCopy>> GetCopiesByBookIdAsync(Guid bookId);
        Task<PagedResult<BookCopy>> GetPagedCopiesAsync(int page, int pageSize);
    }
}
