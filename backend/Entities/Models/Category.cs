using System;
using System.ComponentModel.DataAnnotations;

namespace Erp_sistemi1.Models
{
    public enum CategoryType
    {
        Drinks,
        Foods,
        Electronics,
        Other
    }

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
