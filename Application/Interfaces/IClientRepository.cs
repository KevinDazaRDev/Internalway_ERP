using Amway.Models;

namespace Amway.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(long id);
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task<bool> DeleteAsync(long id);
    }
}
