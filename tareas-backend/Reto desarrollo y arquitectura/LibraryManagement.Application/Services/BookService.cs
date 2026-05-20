using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;


namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Category> _categoryRepository;

        public BookService(IBookRepository bookRepository, IRepository<Author> authorRepository, IRepository <Category> categoryRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task <IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book, List<Guid> authorIds, List<Guid> categoryIds)
        {
            // Obtener autores y categorías existentes de la BD
            var authors = await _authorRepository.FindAsync(a => authorIds.Contains(a.Id));
            var categories = await _categoryRepository.FindAsync(c => categoryIds.Contains(c.Id));

            // Asignar las colecciones
            book.Authors = authors.ToList();
            book.Categories = categories.ToList();

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book, List<Guid> authorIds, List<Guid> categoryIds)
        {
            var existingBook = await _bookRepository.GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found");
            }
            // Actualizar propiedades simples
            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.PublicationYear = book.PublicationYear;
            existingBook.Publisher = book.Publisher;
            existingBook.Description = book.Description;

            // Actualizar relaciones
            var authors = await _authorRepository.FindAsync(a => authorIds.Contains(a.Id));
            var categories = await _categoryRepository.FindAsync(c => categoryIds.Contains(c.Id));

            existingBook.Authors = authors.ToList();
            existingBook.Categories = categories.ToList();

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _bookRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _bookRepository.GetBooksByAuthorAsync(authorId);
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId)
        {
            return await _bookRepository.GetBooksByCategoryAsync(categoryId);
        }

        public async Task<PagedResult<Book>> GetPagedBooksAsync(int page, int pageSize)
        {
            return await _bookRepository.GetPagedAsync(page, pageSize);
        }

    }
}
