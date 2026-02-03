using Amway.Application.Dtos;

namespace Amway.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto?> GetByIdAsync(long id);
        Task<ProductReadDto> CreateAsync(ProductCreateDto dto);
        Task<bool> UpdateAsync(long id, ProductUpdateDto dto);
        Task<bool> DeleteAsync(long id);
        Task<(int Inserted, List<string> MissingBrands, List<string> MissingCategories)> BulkCreateAsync(List<ProductBulkCreateDto> items);
    }
}
