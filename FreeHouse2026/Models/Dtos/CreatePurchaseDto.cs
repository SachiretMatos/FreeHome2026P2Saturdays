namespace FreeHouse2026.Models.Dtos
{
    public class CreatePurchaseDto
    {
        public int ShoppingListId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}