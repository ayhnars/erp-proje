using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class ResetPasswordDto
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Password is required.")]
        public String? Email { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must be match.")]
        public String? ConfirmPassword { get; init; }
    }
}