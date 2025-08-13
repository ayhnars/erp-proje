using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.UserDtos;

public class ErpUserDtoforRegister
{
    [Required(ErrorMessage = "isim zorunludur.")]
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "soyisim zorunludur.")]
    public string LastName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Email zorunludur.")]
    public string Email { get; init; } = string.Empty;


    [Required(ErrorMessage = "şirket adı zorunludur.")]
    public string CompanyName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    public string PhoneNumber { get; init; }

    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string Password { get; init; } = string.Empty;

    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; init; } = string.Empty;

    public bool isBoss { get; init; } = false;

    public string RegistrationKey { get; init; } = string.Empty;

}
