using System.ComponentModel.DataAnnotations;

namespace Tripeace.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "AccountIsRequired")]
        [Display(Name = "Account")]
        public string Account { get; set; }

        [Required(ErrorMessage = "PasswordIsRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
