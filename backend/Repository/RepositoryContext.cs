using Entities;
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
        }
}
