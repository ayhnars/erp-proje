using System.Data.Entity;
using Erp_sistemi1.Models;

namespace Erp_sistemi1.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
