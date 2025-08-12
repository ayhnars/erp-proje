using Entities.Dtos.CompanyDtos;

namespace Entities.Dtos
{
    public class CompanyDtoForCreate : CompanyDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
