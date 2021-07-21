using System.ComponentModel.DataAnnotations;

namespace Web.Components.Data.User
{
    public class LoginData
    {
        [Required(ErrorMessage = "Complete this field.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Complete this field.")]
        public string Password { get; set; }
    }
}