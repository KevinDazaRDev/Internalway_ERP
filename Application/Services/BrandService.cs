using Amway.Application.Dtos;
using Amway.Application.Interfaces;

namespace Amway.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brands;

        public BrandService(IBrandRepository brands)
        {
            _brands = brands;
        }

        public async Task<List<BrandReadDto>> GetAllAsync()
        {
            var items = await _brands.GetAllAsync();
            return items.Select(b => new BrandReadDto
            {
                Id = b.Id,
                Name = b.Name,
                Slug = b.Slug,
                Description = b.Description,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToList();
        }

        public async Task<BrandReadDto?> GetByIdAsync(long id)
        {
            var item = await _brands.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }

            return new BrandReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                Description = item.Description,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public async Task<BrandReadDto> CreateAsync(BrandCreateDto dto)
        {
            var now = DateTime.UtcNow;
            var brand = new Amway.Models.Brand
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await _brands.AddAsync(brand);
            return new BrandReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Slug = created.Slug,
                Description = created.Description,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(long id, BrandUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return false;
            }

            var existing = await _brands.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = dto.Name;
            existing.Slug = dto.Slug;
            existing.Description = dto.Description;
            existing.UpdatedAt = DateTime.UtcNow;

            await _brands.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _brands.DeleteAsync(id);
        }
    }
}
