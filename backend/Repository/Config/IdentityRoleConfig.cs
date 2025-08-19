using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole() { Id = "1", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole() { Id = "2", Name = "Moderator", NormalizedName = "MODERATOR" },
                new IdentityRole() { Id = "3", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "4", Name = "Worker", NormalizedName = "WORKER" }
            );
        }
    }
}