using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        public int ItemID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        // Ürüne FK; Product tablon yoksa FK attribute'u opsiyonel bırakabilirsin
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        // Navigation (opsiyonel)
        // public Order? Order { get; set; }
        // public Product? Product { get; set; }
    }
}
