using Amway.Application.Interfaces;
using Amway.Data;
using Amway.Models;
using Microsoft.EntityFrameworkCore;

namespace Amway.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _db;

        public BrandRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _db.Brands
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(long id)
        {
            return await _db.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Dictionary<string, long>> GetIdBySlugsAsync(IEnumerable<string> slugs)
        {
            var slugList = slugs.Distinct().ToList();
            return await _db.Brands
                .AsNoTracking()
                .Where(b => slugList.Contains(b.Slug))
                .ToDictionaryAsync(b => b.Slug, b => b.Id);
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
            return brand;
        }

        public async Task UpdateAsync(Brand brand)
        {
            _db.Brands.Update(brand);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _db.Brands.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _db.Brands.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
