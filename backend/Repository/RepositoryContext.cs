using System.Reflection;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<ErpUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public DbSet<Modules> Modules { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Yeni tablolar
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // İlişkiler
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.StockMovements)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.StockMovements)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductID);

            // Decimal hassasiyetleri
            modelBuilder.Entity<Product>()
                .Property(p => p.BuyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.SellPrice)
                .HasPrecision(18, 2);
        }
    }
}
