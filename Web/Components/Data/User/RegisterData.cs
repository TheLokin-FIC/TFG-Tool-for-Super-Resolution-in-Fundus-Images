using System.ComponentModel.DataAnnotations;

namespace Web.Components.Data.User
{
    public class RegisterData
    {
        [Required(ErrorMessage = "Complete this field.")]
        [MinLength(4, ErrorMessage = "Username too short.")]
        [StringLength(24, ErrorMessage = "Username too long.")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Don't use special characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Complete this field.")]
        [MinLength(6, ErrorMessage = "Password too short.")]
        [StringLength(24, ErrorMessage = "Password too long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Complete this field.")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must match.")]
        public string ConfirmPassword { get; set; }
    }
}