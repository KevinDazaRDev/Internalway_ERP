using Amway.Application.Dtos;
using Amway.Application.Interfaces;

namespace Amway.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categories;

        public CategoryService(ICategoryRepository categories)
        {
            _categories = categories;
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var items = await _categories.GetAllAsync();
            return items.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                ParentId = c.ParentId,
                Name = c.Name,
                Slug = c.Slug,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(long id)
        {
            var item = await _categories.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }

            return new CategoryReadDto
            {
                Id = item.Id,
                ParentId = item.ParentId,
                Name = item.Name,
                Slug = item.Slug,
                Description = item.Description,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
        {
            var now = DateTime.UtcNow;
            var category = new Amway.Models.Category
            {
                ParentId = dto.ParentId,
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await _categories.AddAsync(category);
            return new CategoryReadDto
            {
                Id = created.Id,
                ParentId = created.ParentId,
                Name = created.Name,
                Slug = created.Slug,
                Description = created.Description,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(long id, CategoryUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return false;
            }

            var existing = await _categories.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = dto.Name;
            existing.Slug = dto.Slug;
            existing.Description = dto.Description;
            existing.ParentId = dto.ParentId;
            existing.UpdatedAt = DateTime.UtcNow;

            await _categories.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _categories.DeleteAsync(id);
        }
    }
}
