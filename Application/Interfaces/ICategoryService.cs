using Amway.Application.Dtos;

namespace Amway.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryReadDto>> GetAllAsync();
        Task<CategoryReadDto?> GetByIdAsync(long id);
        Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(long id, CategoryUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
