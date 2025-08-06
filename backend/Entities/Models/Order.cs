using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using erpapi.Models;

namespace erpapi.Models
{
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        BankTransfer,
        EFT
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime DeliveryDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        // Navigation Properties (isteğe bağlı)
        // public Company Company { get; set; }
        // public Customer Customer { get; set; }
    }
}
