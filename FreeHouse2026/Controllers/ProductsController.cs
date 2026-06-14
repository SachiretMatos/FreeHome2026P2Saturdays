using FreeHouse2026.Models.Dtos;
using FreeHouse2026.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeHouse2026.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Milk", CategoryId = 1, Price = 1.50m },
            new Product { Id = 2, Name = "Detergent", CategoryId = 2, Price = 3.25m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var result = _products.Select(p => new ProductDto { Id = p.Id, Name = p.Name, CategoryId = p.CategoryId, Price = p.Price });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var p = _products.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(new ProductDto { Id = p.Id, Name = p.Name, CategoryId = p.CategoryId, Price = p.Price });
        }

        [HttpPost]
        public ActionResult<ProductDto> Create(CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required.");
            if (dto.Price < 0) return BadRequest("Price must be non-negative.");

            var id = _products.Any() ? _products.Max(x => x.Id) + 1 : 1;
            var ent = new Product { Id = id, Name = dto.Name, CategoryId = dto.CategoryId, Price = dto.Price };
            _products.Add(ent);
            var result = new ProductDto { Id = ent.Id, Name = ent.Name, CategoryId = ent.CategoryId, Price = ent.Price };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateProductDto dto)
        {
            var existing = _products.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.CategoryId = dto.CategoryId;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _products.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            _products.Remove(existing);
            return NoContent();
        }
    }
}