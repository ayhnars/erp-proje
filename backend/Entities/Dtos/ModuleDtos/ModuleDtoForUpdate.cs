using System.ComponentModel.DataAnnotations;

public class ModuleDtoForUpdate
{
    [Required(ErrorMessage = "Modül adı boş olamaz")]
    public string ModuleName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama boş olamaz")]
    public string ModuleDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Icon boş olamaz")]
    public string Icon { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fiyat boş olamaz")]
    public string Price { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
