using System.ComponentModel.DataAnnotations;
using Entities.Dtos.CompanyDtos;

namespace Entities.Dtos
{
    public class CompanyDtoForCreate
    {
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Şirket adı gereklidir.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vergi numarası gereklidir.")]
        public string TaxNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres gereklidir.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        public string Phone { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
