using ServiciosEInyeccionDependencias.Application.Interfaces;
using ServiciosEInyeccionDependencias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosEInyeccionDependencias.Application.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        // Lista estática: los datos persisten entre peticiones
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        // Constructor: agrega datos de ejemplo si la lista está vacía
        public InMemoryBookRepository()
        {
            if (!_books.Any())
            {
                _books.Add(new Book
                {
                    Id = _nextId++,
                    Title = "Cien años de soledad",
                    Author = "Gabriel García Márquez",
                    ISBN = "978-84-376-0494-7",
                    PublicationYear = 1967,
                    IsAvailable = true,
                    CreatedAt = DateTime.Now
                });

                _books.Add(new Book
                {
                    Id = _nextId++,
                    Title = "El amor en los tiempos del cólera",
                    Author = "Gabriel García Márquez",
                    ISBN = "978-84-376-0495-4",
                    PublicationYear = 1985,
                    IsAvailable = true,
                    CreatedAt = DateTime.Now
                });
            }
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return Task.FromResult(_books.AsEnumerable());
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            var books = _books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(books);
        }

        public Task<Book> AddAsync(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
            return Task.FromResult(book);
        }

        public Task UpdateAsync(Book book)
        {
            var index = _books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                _books[index] = book;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
            var exists = _books.Any(b => b.Id == id);
            return Task.FromResult(exists);
        }
    }
}