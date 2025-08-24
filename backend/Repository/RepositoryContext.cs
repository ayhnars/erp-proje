// Repository/RepositoryContext.cs
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Entities;                 // ErpUser
using Entities.Models;          // OrderItem, Modules, Order, ModuleCart, Company

// Order alias
using OrderEntity = Entities.Models.Order;

namespace Repository
{
    public class RepositoryContext
        : IdentityDbContext<ErpUser, IdentityRole, string>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options) { }

        // ---------- DbSet'ler ----------
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Modules> Modules { get; set; } = default!;
        public DbSet<OrderEntity> Orders { get; set; } = default!;
        public DbSet<ModuleCart> ModuleCarts { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!; // FK hedefi

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Varsa IEntityTypeConfiguration<> sınıflarını uygula
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // ---- ModuleCart fluent mapping ----
            modelBuilder.Entity<ModuleCart>(b =>
            {
                b.ToTable("ModuleCarts");
                b.HasKey(x => x.CartID);

                // Sütun adı DB'de "UserID" olsun (modelde property "UserId")
                b.Property(x => x.UserId).HasColumnName("UserID").IsRequired();

                b.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
                b.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
                b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // İlişkiler
                b.HasOne(x => x.User)
                 .WithMany()
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Company)
                 .WithMany()
                 .HasForeignKey(x => x.CompanyID)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => x.UserId);
                b.HasIndex(x => x.CompanyID);
            });
        }
    }
}
