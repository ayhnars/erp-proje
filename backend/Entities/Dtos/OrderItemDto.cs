namespace Entities.Dtos
{
    public class OrderItemDto
    {
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // UI için opsiyonel:
        public string? ProductName { get; set; }
        public string? UnitName { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}