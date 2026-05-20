using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(Guid id);
        Task<Author> CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Guid Id);
        Task<PagedResult<Author>> GetPagedAuthorsAsync(int page, int pageSize);
    }
}
