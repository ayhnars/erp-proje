// Repository/RepositoryContext.cs
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities;                 // ErpUser burada
using Entities.Models;          // OrderItem, Modules, Order (vb.)

// Alias: Order ismini netleştir
using OrderEntity = Entities.Models.Order;

namespace Repository
{
    public class RepositoryContext
        : IdentityDbContext<ErpUser, IdentityRole, string>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options) { }

        // DbSet'ler
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Modules> Modules { get; set; } = default!;
        public DbSet<OrderEntity> Orders { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity tabloları + kendi modellerin
            base.OnModelCreating(modelBuilder);

            // (Opsiyonel) Varsayılan şema
            // modelBuilder.HasDefaultSchema("dbo");

            // Bu assembly içindeki tüm IEntityTypeConfiguration<> sınıflarını uygula
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
 