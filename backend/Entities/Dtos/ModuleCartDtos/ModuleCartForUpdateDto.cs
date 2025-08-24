namespace Entities.Dtos
{
    public class ModuleCartForUpdateDto
    {
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
