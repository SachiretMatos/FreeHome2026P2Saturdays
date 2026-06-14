namespace FreeHouse2026.Models.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPending { get; set; }
    }
}