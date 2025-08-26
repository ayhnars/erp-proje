using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        public int CompanyID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //  Navigation Property (Bir müşteri birçok stok hareketine sahip olabilir)
        public ICollection<StockMovement> StockMovements { get; set; }
    }
}
