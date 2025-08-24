namespace Entities.Dtos
{
    public class ModuleCartForCreateDto
    {
        public string UserId { get; set; } = default!;
        public int CompanyID { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // "Paid" | "Cancelled" da gelebilir
    }
}
