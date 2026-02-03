using Amway.Application.Dtos;
using Amway.Application.Interfaces;

namespace Amway.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _products;
        private readonly IBrandRepository _brands;
        private readonly ICategoryRepository _categories;

        public ProductService(IProductRepository products, IBrandRepository brands, ICategoryRepository categories)
        {
            _products = products;
            _brands = brands;
            _categories = categories;
        }

        public async Task<List<ProductReadDto>> GetAllAsync()
        {
            var items = await _products.GetAllAsync();
            return items.Select(p => new ProductReadDto
            {
                Id = p.Id,
                BrandId = p.BrandId,
                BrandName = p.Brand?.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                Name = p.Name,
                Slug = p.Slug,
                Sku = p.Sku,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }

        public async Task<ProductReadDto?> GetByIdAsync(long id)
        {
            var item = await _products.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }

            return new ProductReadDto
            {
                Id = item.Id,
                BrandId = item.BrandId,
                BrandName = item.Brand?.Name,
                CategoryId = item.CategoryId,
                CategoryName = item.Category?.Name,
                Name = item.Name,
                Slug = item.Slug,
                Sku = item.Sku,
                Description = item.Description,
                Price = item.Price,
                Currency = item.Currency,
                IsActive = item.IsActive,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var now = DateTime.UtcNow;
            var product = new Amway.Models.Product
            {
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Slug = dto.Slug,
                Sku = dto.Sku,
                Description = dto.Description,
                Price = dto.Price,
                Currency = dto.Currency,
                IsActive = dto.IsActive,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await _products.AddAsync(product);
            return new ProductReadDto
            {
                Id = created.Id,
                BrandId = created.BrandId,
                BrandName = created.Brand?.Name,
                CategoryId = created.CategoryId,
                CategoryName = created.Category?.Name,
                Name = created.Name,
                Slug = created.Slug,
                Sku = created.Sku,
                Description = created.Description,
                Price = created.Price,
                Currency = created.Currency,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<(int Inserted, List<string> MissingBrands, List<string> MissingCategories)> BulkCreateAsync(List<ProductBulkCreateDto> items)
        {
            if (items == null || items.Count == 0)
            {
                return (0, new List<string>(), new List<string>());
            }

            var brandSlugs = items.Select(i => i.BrandSlug).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            var categorySlugs = items.Select(i => i.CategorySlug).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            var brandMap = await _brands.GetIdBySlugsAsync(brandSlugs);
            var categoryMap = await _categories.GetIdBySlugsAsync(categorySlugs);

            var missingBrands = brandSlugs.Where(s => !brandMap.ContainsKey(s)).ToList();
            var missingCategories = categorySlugs.Where(s => !categoryMap.ContainsKey(s)).ToList();

            if (missingBrands.Count > 0 || missingCategories.Count > 0)
            {
                return (0, missingBrands, missingCategories);
            }

            var now = DateTime.UtcNow;
            var products = items.Select(i => new Amway.Models.Product
            {
                BrandId = brandMap[i.BrandSlug],
                CategoryId = categoryMap[i.CategorySlug],
                Name = i.Name,
                Slug = i.Slug,
                Sku = i.Sku,
                Description = i.Description,
                Price = i.Price,
                Currency = i.Currency,
                IsActive = i.IsActive,
                CreatedAt = now,
                UpdatedAt = now
            }).ToList();

            await _products.AddRangeAsync(products);
            return (products.Count, new List<string>(), new List<string>());
        }

        public async Task<bool> UpdateAsync(long id, ProductUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return false;
            }

            var existing = await _products.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.BrandId = dto.BrandId;
            existing.CategoryId = dto.CategoryId;
            existing.Name = dto.Name;
            existing.Slug = dto.Slug;
            existing.Sku = dto.Sku;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Currency = dto.Currency;
            existing.IsActive = dto.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _products.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _products.DeleteAsync(id);
        }
    }
}
