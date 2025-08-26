using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int CompanyID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required, StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        [StringLength(50)]
        public string UnitType { get; set; }

        public int MinStockLevel { get; set; }

        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }

        public string ProductDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property (Bir ürün bir kategoriye ait)
        public Category Category { get; set; }

        // Navigation Property (Bir ürünün birçok stok hareketi olabilir)
        public ICollection<StockMovement> StockMovements { get; set; }
    }
}
