using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    // S�n�f ad�n� .NET kural�na g�re d�zelttim: ErpUserDtoForRegister
    public class ErpUserDtoForRegister : ErpUserDto
    {
        // Zorunlu alanlar i�in required kullan
        [Required]
        public string ConfirmPassword { get; init; } = string.Empty;

        // �irket ad� ve imza kodu opsiyonelse nullable yap;
        // opsiyonel de�illerse '?' kald�r�p string.Empty ba�lat.
        public string? CompanyName { get; init; }

        // PascalCase: IsBoss
        public bool IsBoss { get; init; } = false;

        public string? SignCode { get; init; }
    }
}
