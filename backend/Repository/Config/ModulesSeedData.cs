using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ModulesConfig : IEntityTypeConfiguration<Modules>
{
    public void Configure(EntityTypeBuilder<Modules> builder)
    {
        builder.HasData(
            new Modules
            {
                Id = 1,
                ModuleName = "Stok Takibi",
                ModuleDescription = "Depo ve ürün yönetimi",
                Icon = "fa-boxes",
                Price = "149.99",
                CreatedAt = new DateTime(2023, 1, 1),
                IsActive = true
            },
            new Modules
            {
                Id = 2,
                ModuleName = "Muhasebe",
                ModuleDescription = "Fatura ve kasa yönetimi",
                Icon = "fa-calculator",
                Price = "249.99",
                CreatedAt = new DateTime(2023, 1, 1),
                IsActive = true
            }
        );
    }
}
