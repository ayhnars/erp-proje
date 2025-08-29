using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Ödeme yöntemi – Order/Payment her yerde aynı enum kullanılmalı (tek tanım!)
    public enum PaymentMethod
    {
        Cash = 0,
        CreditCard = 1,
        BankTransfer = 2,
        VirtualPOS = 3
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }

    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }                  // [pk, identity]

        [ForeignKey(nameof(ModuleCart))]
        public int CartID { get; set; }                     // FK -> ModuleCarts.CartID

        // Varsayılan UTC önerilir (raporlama ve timezone sorunları için)
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Enum’lar DB’de string olsun istiyorsan OnModelCreating’de conversion ekle (aşağıda)
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string? CardToken { get; set; }

        // Navigation
        public ModuleCart? ModuleCart { get; set; }
    }
}
