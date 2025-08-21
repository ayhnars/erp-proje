using Erp_sistemi1.Models;
using System.Data.Entity;

namespace Erp_sistemi1.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
    }
}
