
using System.ComponentModel.DataAnnotations;

namespace Authorization.ViewModels
{

    public class UserSignInVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}