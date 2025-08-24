namespace Entities.Dtos
{
    public class ModuleCartDto
    {
        public int CartID { get; set; }
        public string UserId { get; set; } = default!;
        public int CompanyID { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
    }
}