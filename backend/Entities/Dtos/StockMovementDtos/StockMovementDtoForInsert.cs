namespace Entities.Dtos.StockMovementDtos
{
    public class StockMovementDtoForInsert
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
