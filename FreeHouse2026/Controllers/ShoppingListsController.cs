using FreeHouse2026.Models.Dtos;
using FreeHouse2026.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeHouse2026.Controllers
{
    [ApiController]
    [Route("api/shoppinglists")]
    public class ShoppingListsController : ControllerBase
    {
        private static readonly List<ShoppingList> _lists = new()
        {
            new ShoppingList { Id = 1, Name = "Weekly Groceries" },
            new ShoppingList { Id = 2, Name = "Party Supplies" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ShoppingListDto>> GetAll()
        {
            var result = _lists.Select(l => new ShoppingListDto { Id = l.Id, Name = l.Name, Created = l.Created, IsCompleted = l.IsCompleted });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListDto> GetById(int id)
        {
            var l = _lists.FirstOrDefault(x => x.Id == id);
            if (l == null) return NotFound();
            return Ok(new ShoppingListDto { Id = l.Id, Name = l.Name, Created = l.Created, IsCompleted = l.IsCompleted });
        }

        [HttpPost]
        public ActionResult<ShoppingListDto> Create(CreateShoppingListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required.");
            var id = _lists.Any() ? _lists.Max(x => x.Id) + 1 : 1;
            var ent = new ShoppingList { Id = id, Name = dto.Name };
            _lists.Add(ent);
            var result = new ShoppingListDto { Id = ent.Id, Name = ent.Name, Created = ent.Created, IsCompleted = ent.IsCompleted };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateShoppingListDto dto)
        {
            var existing = _lists.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _lists.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            _lists.Remove(existing);
            return NoContent();
        }
    }
}