using Microsoft.AspNetCore.Identity;

namespace Entities
{
    // ErpUser Kullanıcının Sahip olacağı propları belirleyeceğimiz yer.
    //Login ve Register ve diğer her şey için bir DTO oluşturun.
    public class ErpUser : IdentityUser
    {

        public ErpUser()
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                UserName = Email;
            }
        }

        //Telefon numarası ve E-posta IdentityUser tarafından sağlanıyor.
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? CompanyName { get; set; } = string.Empty;

        public string? RegistrationKey { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastActivityTime { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();



    }


}
