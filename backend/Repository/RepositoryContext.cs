using System.Reflection;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD

=======
using Entities;                 // ErpUser burada
using Entities.Models;          // OrderItem, Modules, Order (vb.)

// Alias: Order ismini netleştir
using OrderEntity = Entities.Models.Order;
>>>>>>> dev

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<ErpUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration(new ProductConfig());
            // modelBuilder.ApplyConfiguration(new CategoryConfig());

=======
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
>>>>>>> dev
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
