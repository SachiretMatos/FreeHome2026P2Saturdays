using FreeHouse2026.Models.Dtos;
using FreeHouse2026.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeHouse2026.Controllers
{
    [ApiController]
    [Route("api/purchases")]
    public class PurchasesController : ControllerBase
    {
        private static readonly List<Purchase> _purchases = new()
        {
            new Purchase { Id = 1, ShoppingListId = 1, ProductId = 1, Quantity = 2, UnitPrice = 1.50m },
            new Purchase { Id = 2, ShoppingListId = 1, ProductId = 2, Quantity = 1, UnitPrice = 3.25m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<PurchaseDto>> GetAll()
        {
            var result = _purchases.Select(p => new PurchaseDto { Id = p.Id, ShoppingListId = p.ShoppingListId, ProductId = p.ProductId, Quantity = p.Quantity, UnitPrice = p.UnitPrice, IsPending = p.IsPending });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PurchaseDto> GetById(int id)
        {
            var p = _purchases.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(new PurchaseDto { Id = p.Id, ShoppingListId = p.ShoppingListId, ProductId = p.ProductId, Quantity = p.Quantity, UnitPrice = p.UnitPrice, IsPending = p.IsPending });
        }

        [HttpPost]
        public ActionResult<PurchaseDto> Create(CreatePurchaseDto dto)
        {
            if (dto.Quantity <= 0) return BadRequest("Quantity must be greater than zero.");
            if (dto.UnitPrice < 0) return BadRequest("UnitPrice must be non-negative.");

            var id = _purchases.Any() ? _purchases.Max(x => x.Id) + 1 : 1;
            var ent = new Purchase { Id = id, ShoppingListId = dto.ShoppingListId, ProductId = dto.ProductId, Quantity = dto.Quantity, UnitPrice = dto.UnitPrice };
            _purchases.Add(ent);
            var result = new PurchaseDto { Id = ent.Id, ShoppingListId = ent.ShoppingListId, ProductId = ent.ProductId, Quantity = ent.Quantity, UnitPrice = ent.UnitPrice, IsPending = ent.IsPending };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreatePurchaseDto dto)
        {
            var existing = _purchases.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Quantity = dto.Quantity;
            existing.UnitPrice = dto.UnitPrice;
            existing.IsPending = dto.ShoppingListId == 0 ? true : existing.IsPending;
            existing.ShoppingListId = dto.ShoppingListId;
            existing.ProductId = dto.ProductId;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _purchases.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            _purchases.Remove(existing);
            return NoContent();
        }
    }
}