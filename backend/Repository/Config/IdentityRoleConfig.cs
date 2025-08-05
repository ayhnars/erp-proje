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
                new IdentityRole(){ Name="Manager", NormalizedName="MANAGER"},
                new IdentityRole(){ Name="Moderator", NormalizedName="MODERATOR"},
                new IdentityRole(){ Name="Admin", NormalizedName="ADMIN"},
                new IdentityRole(){ Name="Worker", NormalizedName="WORKER"}
            );
        }
    }
}