using Tripeace.Domain.Consts;
using System.ComponentModel.DataAnnotations;

namespace Tripeace.Application.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "AccountNameIsRequired")]
        [RegularExpression(@"^[a-zA-Z\d]+$", ErrorMessage = "ContainsUnkowCharacters")]
        [StringLength(AccountInfo.AccountMaxLength, ErrorMessage = "AccountLentgh", MinimumLength = AccountInfo.AccountMinLength)]
        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "PasswordIsRequired")]
        [DataType(DataType.Password)]
        [StringLength(AccountInfo.PasswordMaxLength, ErrorMessage = "PasswordLentgh", MinimumLength = AccountInfo.PasswordMinLength)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PasswordDoesNotMatch")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "EmailIsRequired")]
        [EmailAddress(ErrorMessage = "EmailIsNotValid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "News")]
        public bool AgreeReciveNews { get; set; }
    }
}
