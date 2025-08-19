using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    // Sýnýf adýný .NET kuralýna göre düzelttim: ErpUserDtoForRegister
    public class ErpUserDtoForRegister : ErpUserDto
    {
        // Zorunlu alanlar için required kullan
        [Required]
        public string ConfirmPassword { get; init; } = string.Empty;

        // Þirket adý ve imza kodu opsiyonelse nullable yap;
        // opsiyonel deðillerse '?' kaldýrýp string.Empty baþlat.
        public string? CompanyName { get; init; }

        // PascalCase: IsBoss
        public bool IsBoss { get; init; } = false;

        public string? SignCode { get; init; }
    }
}
