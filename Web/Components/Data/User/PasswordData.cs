using System.ComponentModel.DataAnnotations;
using Web.Components.Validation;

namespace Web.Components.Data.User
{
    public class PasswordData
    {
        [Required(ErrorMessage = "Complete this field.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Complete this field.")]
        [MinStringTrimLength(6, ErrorMessage = "Password too short.")]
        [MaxStringTrimLength(24, ErrorMessage = "Password too long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Complete this field.")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must match.")]
        public string ConfirmPassword { get; set; }
    }
}