using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Entities.Dtos.CompanyDtos
{
    public class CompanyDtoForUpdate
    {
        // Buraya eğer güncellenmesini istemediğin alanlar varsa override edebilirsin
        // Mesela RegistrationDate'nin güncellenmesini istemiyorsan:

        public DateTime RegistrationDate { get; }  // set yok, sadece get var

        public int CompanyId { get; init; }  // id zorunlu

        [Required(ErrorMessage = "Şirket adı Boş kalamaz.")]
        public string CompanyName { get; init; } = null!;

        [Required(ErrorMessage = "Vergi numarası Boş kalamaz.")]
        public string? TaxNumber { get; init; }

        [Required(ErrorMessage = "Adres Boş kalamaz.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Telefon numarası Boş kalamaz.")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; init; }

        // RegistrationDate kaldırdım ki değiştirilmesin
    }
}