namespace Entities.Models
{
    // Entity modeli
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}