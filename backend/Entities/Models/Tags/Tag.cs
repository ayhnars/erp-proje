using Entities;
using Entities.Models;

public class Tag
{
    public int TagId { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }  // Navigation

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<TagModuleAccess> ModuleAccesses { get; set; }
    public ICollection<ErpUser> Users { get; set; }
}
