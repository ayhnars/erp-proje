namespace Entities.Dtos.PaymentDtos
{
    public class PaymentForUpdateDto
    {
        public string PaymentStatus { get; set; } = default!; // "Success" | "Failed" | "Pending"
    }
}
