namespace Entities.Dtos.CompanyDtos
{
    public class CompanyUpdateDto : CompanyDto
    {
        // Buraya eğer güncellenmesini istemediğin alanlar varsa override edebilirsin
        // Mesela RegistrationDate'nin güncellenmesini istemiyorsan:

        public new DateTime RegistrationDate { get; }  // set yok, sadece get var

        public new int CompanyID { get; init; }  // id zorunlu

        public new string CompanyName { get; init; } = null!;
        public new string? TaxNumber { get; init; }
        public new string? Address { get; init; }
        public new string? Phone { get; init; }

        // RegistrationDate kaldırdım ki değiştirilmesin
    }
}