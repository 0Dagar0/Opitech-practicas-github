using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository :Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _dbSet.Where(b => b.Authors.Any(a => a.Id == authorId)).ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId)
        {
            return await _dbSet.Where(b => b.Categories.Any(c => c.Id == categoryId)).ToListAsync();
        }

        public async Task<Book?> GetBookWithCategoriesAsync(Guid bookId)
        {
            return await _dbSet
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }
    }
}


