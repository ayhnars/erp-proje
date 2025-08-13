using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
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

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        // Sipariş oluşturulduğunda otomatik dolsun
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Teslim tarihi daha sonra belli olabilir -> nullable yap
        public DateTime? DeliveryDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Navigation (istersen açarsın)
        // public Company? Company { get; set; }
        // public Customer? Customer { get; set; }
        // public ICollection<OrderItem>? Items { get; set; }
    }
}
