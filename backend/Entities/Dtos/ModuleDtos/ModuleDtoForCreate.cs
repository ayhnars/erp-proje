using System.ComponentModel.DataAnnotations;

public class ModuleDtoForCreate
{
    [Required(ErrorMessage = "Modül adı boş olamaz")]
    public string ModuleName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama boş olamaz")]
    public string ModuleDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Icon boş olamaz")]
    public string Icon { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fiyat boş olamaz")]
    public string Price { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}
