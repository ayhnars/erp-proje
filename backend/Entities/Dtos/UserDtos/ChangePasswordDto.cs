using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.UserDtos;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Eski şifre boş olamaz")]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre boş olamaz")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Yeni şifre en az 6 karakter olmalı")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre tekrarı boş olamaz")]
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
