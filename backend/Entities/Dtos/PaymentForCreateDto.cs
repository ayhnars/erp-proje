namespace Entities.Dtos.PaymentDtos;
using Entities.Models;


    public class PaymentForCreateDto
    {
        public int CartID { get; set; }
        public DateTime? PaymentDate { get; set; }          // null gelirse now
        public string PaymentMethod { get; set; } = default!; // "CreditCard", "BankTransfer", "VirtualPOS"
        public string PaymentStatus { get; set; } = "Pending";
        public string? CardToken { get; set; }              // gateway’den gelen token
    }

