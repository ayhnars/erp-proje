namespace Entities.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string UnitType { get; set; }
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
