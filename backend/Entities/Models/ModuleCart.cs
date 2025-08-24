using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities; // ErpUser

namespace Entities.Models
{
    public enum CartStatus
    {
        Pending = 0,
        Paid = 1,
        Cancelled = 2
    }

    public class ModuleCart
    {
        public int CartID { get; set; }

        // ---- FK alanları (tek ve net) ----
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;   // AspNetUsers.Id (string)

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }

        // ---- Diğer kolonlar ----
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public CartStatus Status { get; set; } = CartStatus.Pending;

        // ---- Navigations ----
        public ErpUser? User { get; set; }
        public Company? Company { get; set; }
    }
}
