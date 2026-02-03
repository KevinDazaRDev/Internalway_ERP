using Amway.Application.Dtos;
using Amway.Application.Interfaces;
using Amway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amway.Controllers.Clients
{
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clients;

        public ClientController(IClientService clients)
        {
            _clients = clients;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientReadDto>>> GetAll()
        {
            var items = await _clients.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ClientReadDto>> GetById(long id)
        {
            var item = await _clients.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ClientReadDto>> Create([FromBody] ClientCreateDto dto)
        {
            var created = await _clients.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] ClientUpdateDto dto)
        {
            var updated = await _clients.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _clients.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
