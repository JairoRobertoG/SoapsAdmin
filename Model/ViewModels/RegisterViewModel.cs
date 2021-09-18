using System.ComponentModel.DataAnnotations;

namespace Soaps.Model.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Contraseña y confirmación de contraseña no concuerdan.")]
        public string ConfirmPassword { get; set; }
    }
}
