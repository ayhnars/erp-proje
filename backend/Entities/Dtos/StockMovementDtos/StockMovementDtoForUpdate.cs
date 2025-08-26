namespace Entities.Dtos.StockMovementDtos
{
    public class StockMovementDtoForUpdate
    {
        public int MovementID { get; set; }
        public int ProductID { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
