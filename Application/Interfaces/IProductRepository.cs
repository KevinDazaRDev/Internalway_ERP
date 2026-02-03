using Amway.Models;

namespace Amway.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(long id);
        Task<Product> AddAsync(Product product);
        Task AddRangeAsync(List<Product> products);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(long id);
    }
}
