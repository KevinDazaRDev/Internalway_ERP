using Amway.Application.Dtos;
using Amway.Application.Interfaces;
using Amway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amway.Controllers.Products
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _products;

        public ProductController(IProductService products)
        {
            _products = products;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetAll()
        {
            var items = await _products.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductReadDto>> GetById(long id)
        {
            var item = await _products.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create([FromBody] ProductCreateDto dto)
        {
            var created = await _products.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] List<ProductBulkCreateDto> items)
        {
            if (items == null || items.Count == 0)
            {
                return BadRequest("Empty payload.");
            }

            var result = await _products.BulkCreateAsync(items);
            if (result.MissingBrands.Count > 0 || result.MissingCategories.Count > 0)
            {
                return BadRequest(new
                {
                    message = "Missing brand/category slugs.",
                    missingBrands = result.MissingBrands,
                    missingCategories = result.MissingCategories
                });
            }

            return Ok(new { inserted = result.Inserted });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] ProductUpdateDto dto)
        {
            var updated = await _products.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _products.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
