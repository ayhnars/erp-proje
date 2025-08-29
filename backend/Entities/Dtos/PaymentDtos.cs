namespace Entities.Dtos.PaymentDtos;
using Entities.Models;


public class PaymentDto
{
    public int PaymentID { get; set; }
    public int CartID { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } = default!;
    public string? CardToken { get; set; }
}
