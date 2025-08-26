namespace Entities.Dtos.StockMovementDtos
{
    public class StockMovementDto
    {
        public int MovementID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string MovementType { get; set; } // In, Out, Correction
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
