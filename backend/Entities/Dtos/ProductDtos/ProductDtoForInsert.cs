namespace Entities.Dtos.ProductDtos
{
    public class ProductDtoForInsert
    {
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string UnitType { get; set; }
        public int MinStockLevel { get; set; }
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
        public string ProductDescription { get; set; }
    }
}
