using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(Guid id);
        Task<Book> CreateBookAsync(Book book, List<Guid> authorIds, List<Guid> categoryIds); 
        Task UpdateBookAsync(Book book, List<Guid> authorsIds, List<Guid> categoryIds);
        Task DeleteBookAsync(Guid id);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
        Task<PagedResult<Book>> GetPagedBooksAsync(int page, int pageSize);
    }
}
