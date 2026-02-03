using Amway.Application.Dtos;

namespace Amway.Application.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandReadDto>> GetAllAsync();
        Task<BrandReadDto?> GetByIdAsync(long id);
        Task<BrandReadDto> CreateAsync(BrandCreateDto dto);
        Task<bool> UpdateAsync(long id, BrandUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
