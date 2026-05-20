using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
        Task<PagedResult<Category>> GetPagedCategoriesAsync(int page, int pageSize);
    }
}