using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.ViewModels.Account
{
    public class Login
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
