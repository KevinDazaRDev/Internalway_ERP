using Amway.Application.Dtos;

namespace Amway.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientReadDto>> GetAllAsync();
        Task<ClientReadDto?> GetByIdAsync(long id);
        Task<ClientReadDto> CreateAsync(ClientCreateDto dto);
        Task<bool> UpdateAsync(long id, ClientUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
