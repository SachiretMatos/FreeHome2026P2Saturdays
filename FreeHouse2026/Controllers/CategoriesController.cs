using FreeHouse2026.Models.Dtos;
using FreeHouse2026.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeHouse2026.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private static readonly List<Category> _categories = new()
        {
            new Category { Id = 1, Name = "Groceries", Description = "Food and beverages" },
            new Category { Id = 2, Name = "Household", Description = "Cleaning and supplies" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAll()
        {
            var result = _categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById(int id)
        {
            var c = _categories.FirstOrDefault(x => x.Id == id);
            if (c == null) return NotFound();
            return Ok(new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description });
        }

        [HttpPost]
        public ActionResult<CategoryDto> Create(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required.");
            var id = _categories.Any() ? _categories.Max(x => x.Id) + 1 : 1;
            var ent = new Category { Id = id, Name = dto.Name, Description = dto.Description };
            _categories.Add(ent);
            var result = new CategoryDto { Id = ent.Id, Name = ent.Name, Description = ent.Description };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateCategoryDto dto)
        {
            var existing = _categories.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _categories.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            _categories.Remove(existing);
            return NoContent();
        }
    }
}