using ServiciosEInyeccionDependencias.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosEInyeccionDependencias.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetByAuthorAsync(string author);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}