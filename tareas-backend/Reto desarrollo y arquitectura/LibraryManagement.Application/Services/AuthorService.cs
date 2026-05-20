using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public  AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }
        
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid Id)
        {
            return await _authorRepository.GetByIdAsync(Id);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _authorRepository.Update(author);
            await _authorRepository.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author != null)
            {
                _authorRepository.Delete(author);
                await _authorRepository.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<Author>> GetPagedAuthorsAsync(int page, int pageSize)
        {
            return await _authorRepository.GetPagedAsync(page, pageSize);
        }

    }
}
