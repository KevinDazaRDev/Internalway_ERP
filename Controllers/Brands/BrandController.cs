using Amway.Application.Dtos;
using Amway.Application.Interfaces;
using Amway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amway.Controllers.Brands
{
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brands;

        public BrandController(IBrandService brands)
        {
            _brands = brands;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandReadDto>>> GetAll()
        {
            var items = await _brands.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<BrandReadDto>> GetById(long id)
        {
            var item = await _brands.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<BrandReadDto>> Create([FromBody] BrandCreateDto dto)
        {
            var created = await _brands.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] BrandUpdateDto dto)
        {
            var updated = await _brands.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _brands.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
