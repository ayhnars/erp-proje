using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities.Models; // Burada kalsın
using OrderEntity = Entities.Models.Order; // ÇAKIŞMAYI ENGELLER

public class RepositoryContext : IdentityDbContext<ErpUser>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
    }

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Modules> Modules { get; set; }

    // BURADA Entities.Models.Order'ı açıkça belirtiyoruz
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
