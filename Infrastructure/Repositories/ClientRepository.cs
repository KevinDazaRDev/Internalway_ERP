using Amway.Application.Interfaces;
using Amway.Data;
using Amway.Models;
using Microsoft.EntityFrameworkCore;

namespace Amway.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _db;

        public ClientRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _db.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(long id)
        {
            return await _db.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> AddAsync(Client client)
        {
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
            return client;
        }

        public async Task UpdateAsync(Client client)
        {
            _db.Clients.Update(client);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _db.Clients.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _db.Clients.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
