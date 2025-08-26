using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
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

        //  Navigation Property (Bir kategoriye bağlı birden çok ürün olabilir)
        public ICollection<Product> Products { get; set; }
    }
}
