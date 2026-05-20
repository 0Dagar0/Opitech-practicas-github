using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
        Task<Book?> GetBookWithCategoriesAsync(Guid bookId);
    }
}
