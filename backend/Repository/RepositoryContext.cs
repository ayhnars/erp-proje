// Repository/RepositoryContext.cs
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Entities;                 // ErpUser
using Entities.Models;          // OrderItem, Modules, Order, ModuleCart, Company, Payment

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
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;

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
                b.Property(x => x.UserId)
                 .HasColumnName("UserID")
                 .IsRequired();

                b.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");

                b.Property(x => x.Status)
                 .HasConversion<string>()
                 .HasMaxLength(20);

                b.Property(x => x.CreatedAt)
                 .HasDefaultValueSql("GETUTCDATE()");

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

            // ---- Payment fluent mapping ----
            modelBuilder.Entity<Payment>(e =>
            {
                e.ToTable("Payments");
                e.HasKey(p => p.PaymentID);

                // İlişki: Payment (n) — (1) ModuleCart
                e.HasOne(p => p.ModuleCart)
                 .WithMany(c => c.Payments)
                 .HasForeignKey(p => p.CartID)
                 .OnDelete(DeleteBehavior.Restrict);

                // Enum’ları string olarak sakla
                e.Property(p => p.PaymentMethod)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .HasColumnType("varchar(20)");

                e.Property(p => p.PaymentStatus)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .HasColumnType("varchar(20)");

                e.Property(p => p.CardToken)
                 .HasMaxLength(256)
                 .HasColumnType("varchar(256)");

                // Opsiyonel: tarih default'u DB tarafında
                // e.Property(p => p.PaymentDate).HasDefaultValueSql("GETUTCDATE()");

                // Performans için index (opsiyonel)
                e.HasIndex(p => p.CartID);
                e.HasIndex(p => p.PaymentStatus);
            });

            // ---- Order fluent mapping ----
            modelBuilder.Entity<OrderEntity>(e =>
            {
                e.ToTable("Orders");

                // PaymentMethod ve Status enum'larını string sakla
                e.Property(o => o.PaymentMethod)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .HasColumnType("varchar(20)");

                e.Property(o => o.Status)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .HasColumnType("varchar(20)");

                // Opsiyonel:
                // e.Property(o => o.OrderDate).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
