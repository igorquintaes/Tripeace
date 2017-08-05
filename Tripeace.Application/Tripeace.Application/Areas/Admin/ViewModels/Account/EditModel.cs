using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Consts;

namespace Tripeace.Application.Areas.Admin.ViewModels.Account
{
    public class EditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AccountNameIsRequired")]
        [RegularExpression(@"^[a-zA-Z\d]+$", ErrorMessage = "ContainsUnkowCharacters")]
        [StringLength(AccountInfo.AccountMaxLength, ErrorMessage = "AccountLentgh", MinimumLength = AccountInfo.AccountMinLength)]
        [Display(Name = "Account")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailIsRequired")]
        [EmailAddress(ErrorMessage = "EmailIsNotValid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool ReciveNews { get; set; }
    }
}
