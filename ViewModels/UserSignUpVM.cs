
using System.ComponentModel.DataAnnotations;


namespace Authorization.ViewModels
{

    public class UserSignUpVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}