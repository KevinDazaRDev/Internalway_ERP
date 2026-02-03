using Amway.Application.Dtos;
using Amway.Application.Interfaces;

namespace Amway.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clients;

        public ClientService(IClientRepository clients)
        {
            _clients = clients;
        }

        public async Task<List<ClientReadDto>> GetAllAsync()
        {
            var items = await _clients.GetAllAsync();
            return items.Select(c => new ClientReadDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                DocumentType = c.DocumentType,
                DocumentNumber = c.DocumentNumber,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        public async Task<ClientReadDto?> GetByIdAsync(long id)
        {
            var item = await _clients.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }

            return new ClientReadDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                DocumentType = item.DocumentType,
                DocumentNumber = item.DocumentNumber,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public async Task<ClientReadDto> CreateAsync(ClientCreateDto dto)
        {
            var now = DateTime.UtcNow;
            var client = new Amway.Models.Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DocumentType = dto.DocumentType,
                DocumentNumber = dto.DocumentNumber,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await _clients.AddAsync(client);
            return new ClientReadDto
            {
                Id = created.Id,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                Phone = created.Phone,
                DocumentType = created.DocumentType,
                DocumentNumber = created.DocumentNumber,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(long id, ClientUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return false;
            }

            var existing = await _clients.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.DocumentType = dto.DocumentType;
            existing.DocumentNumber = dto.DocumentNumber;
            existing.UpdatedAt = DateTime.UtcNow;

            await _clients.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _clients.DeleteAsync(id);
        }
    }
}
