namespace PCComponentInventoryManagement.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } // "Поступление" или "Отгрузка"
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public int Quantity { get; set; }
    }
}