using Amway.Models;

namespace Amway.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(long id);
        Task<Dictionary<string, long>> GetIdBySlugsAsync(IEnumerable<string> slugs);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(long id);
    }
}
