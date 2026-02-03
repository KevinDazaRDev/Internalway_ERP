using Amway.Models;

namespace Amway.Application.Interfaces
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(long id);
        Task<Dictionary<string, long>> GetIdBySlugsAsync(IEnumerable<string> slugs);
        Task<Brand> AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(long id);
    }
}
