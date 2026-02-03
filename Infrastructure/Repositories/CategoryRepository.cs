using Amway.Application.Interfaces;
using Amway.Data;
using Amway.Models;
using Microsoft.EntityFrameworkCore;

namespace Amway.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            return await _db.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Dictionary<string, long>> GetIdBySlugsAsync(IEnumerable<string> slugs)
        {
            var slugList = slugs.Distinct().ToList();
            return await _db.Categories
                .AsNoTracking()
                .Where(c => slugList.Contains(c.Slug))
                .ToDictionaryAsync(c => c.Slug, c => c.Id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _db.Categories.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _db.Categories.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
