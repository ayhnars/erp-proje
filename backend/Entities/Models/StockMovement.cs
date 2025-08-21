using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_sistemi1.Models
{
    [Table("StockMovements")]
    public class StockMovement
    {
        [Key]
        public int MovementID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public string MovementType { get; set; } // In, Out, Correction

        [Required]
        public int Quantity { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
