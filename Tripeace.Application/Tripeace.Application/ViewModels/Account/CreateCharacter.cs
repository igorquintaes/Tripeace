using Tripeace.Application.MVC.DataAnnotations;
using Tripeace.Domain.Consts;
using Tripeace.Domain.Enums;
using Tripeace.Service.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.ViewModels.Account
{
    public class CreateCharacter
    {
        [Required(ErrorMessage = "NameIsRequired")]
        [Display(Name = "Name")]
        [CharacterQuantity(' ', 2, ErrorMessage = "NoMoreThanTwoSpaces")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "ContainsUnkowCharacters")]
        [StringLength(CharacterInfo.NameMaxLength, ErrorMessage = "CharacterNameLentgh", MinimumLength = CharacterInfo.NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = "VocationIsRequired")]
        [Display(Name = "Vocation")]
        public Vocation Vocation { get; set; }

        [Required(ErrorMessage = "SexIsRequired")]
        [Display(Name = "Sex")]
        public Sex Sex { get; set; }

        public IEnumerable<Vocation> AllowedVocations { get; set; }
    }
}
