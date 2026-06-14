namespace FreeHouse2026.Models.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
    }
}