using ServiciosEInyeccionDependencias.Application.Dtos;
using ServiciosEInyeccionDependencias.Application.Interfaces;
using ServiciosEInyeccionDependencias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosEInyeccionDependencias.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        // GET: Obtener todos los libros
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(book => MapToDto(book));
        }

        // GET con filtro: Obtener libros por autor
        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author)
        {
            var books = await _repository.GetByAuthorAsync(author);
            return books.Select(book => MapToDto(book));
        }

        // GET: Obtener libro por ID
        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book != null ? MapToDto(book) : null;
        }

        // POST: Crear nuevo libro
        public async Task<BookDto> CreateBookAsync(CreateBookDto createDto)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(createDto.Title))
                throw new ArgumentException("El título es requerido");

            if (string.IsNullOrWhiteSpace(createDto.Author))
                throw new ArgumentException("El autor es requerido");

            if (createDto.PublicationYear <= 0 || createDto.PublicationYear > DateTime.Now.Year)
                throw new ArgumentException("Año de publicación inválido");

            var book = new Book
            {
                Title = createDto.Title,
                Author = createDto.Author,
                ISBN = createDto.ISBN,
                PublicationYear = createDto.PublicationYear,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            };

            var createdBook = await _repository.AddAsync(book);
            return MapToDto(createdBook);
        }

        // PUT: Actualizar libro completo
        public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateDto)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
                return null;

            existingBook.UpdateDetails(
                updateDto.Title,
                updateDto.Author,
                updateDto.ISBN,
                updateDto.PublicationYear
            );

            await _repository.UpdateAsync(existingBook);
            return MapToDto(existingBook);
        }

        // DELETE: Eliminar libro
        public async Task<bool> DeleteBookAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        // Método privado para mapear Entidad a DTO
        private BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }
    }
}