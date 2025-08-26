using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
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
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }  // Identity kullanıldığı için genelde string olur

        [Required]
        public string MovementType { get; set; } // In, Out, Correction

        [Required]
        public int Quantity { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //  Navigation Properties
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ErpUser User { get; set; } // Identity User
    }
}
